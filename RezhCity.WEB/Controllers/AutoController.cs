﻿using AutoMapper;
using Newtonsoft.Json;
using RezhCity.DAL.Entities;
using RezhCity.DAL.Managers;
using RezhCity.WEB.Models;
using RezhCity.WEB.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
using RezhCity.DAL.Utils;
using RezhCity.DAL.Enums;

namespace RezhCity.WEB.Controllers
{
    public class AutoController : Controller
    {
        DataManager manager;
        public AutoController(DataManager _manager)
        {
            manager = _manager;
        }

        public ActionResult Index(int page = 1, AdvType? type = null, AdvObject? obj = null, string keyword = null)
        {
            var query = type.HasValue || obj.HasValue || !(String.IsNullOrEmpty(keyword)) ?
                manager.SearchAdvQuery(category: Category.Auto, type: type, obj: obj, keyword: keyword) :
                manager.GetAdvertisementsQuery(category: Category.Auto);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advertisement, ShortAdvView>()
                .AfterMap((s, d) =>
                {
                    d.Price = s.Price.HasValue ? s.Price.ToString() : String.Empty;
                    d.Created = "размещено " + s.Created.ToString("dd-MM-yyyy HH:mm");
                    if (!String.IsNullOrEmpty(d.PhoneLink))
                        d.PhoneLink = "tel:" + s.Phone;
                    d.Url = Url.Action("Details", "Auto", new { id = s.Id });
                });
            });

            var pagesCount = manager.GetPagesCount(query);
            query = manager.SetPagingForQuery(query, page);

            IMapper mapper = config.CreateMapper();
            var model = new IndexAutoView()
            {
                Advertisements = mapper.Map<List<ShortAdvView>>(query.ToList()),
                CurrentPage = page,
                PagesCount = pagesCount,
                Keyword = keyword,
                Category = this.ControllerContext.RouteData.Values["controller"].ToString()
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new CreateAutoView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAutoView model, List<HttpPostedFileBase> images)
        {
            if (String.IsNullOrEmpty(model.Phone) && String.IsNullOrEmpty(model.VkId) && String.IsNullOrEmpty(model.OkId))
            {
                ModelState.AddModelError("Phone", "Как с вами связаться? Укажите хотя бы один способ связи:\r\nлибо телефон");
                ModelState.AddModelError("VkId", "либо ВК");
                ModelState.AddModelError("OkId", "либо Одноклассники");
            }

            if (!String.IsNullOrEmpty(model.Phone) && model.Phone.Last() == '_')
                ModelState.AddModelError("Phone", "Кажется, Вы неверно ввели телефон :(");

            if (ModelState.IsValid)
            {
                //mapping
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateAutoView, Advertisement>()
                    .ForMember(dest => dest.Id, opt => opt.UseValue(Guid.NewGuid()))
                    .ForMember(dest => dest.Category, opt => opt.UseValue(Category.Auto))
                    .ForMember(dest => dest.Created, opt => opt.UseValue(DateTime.Now))
                    .ForMember(dest => dest.Premoderated, opt => opt.UseValue(false))
                    .AfterMap((s, d) =>
                    {
                        d.VkId = String.IsNullOrWhiteSpace(s.VkId) ? null : "https://vk.com/" + s.VkId;
                        d.OkId = String.IsNullOrWhiteSpace(s.OkId) ? null : "https://ok.ru/profile/" + s.OkId;
                    });
                });
                IMapper mapper = config.CreateMapper();
                Advertisement advertisement = mapper.Map<Advertisement>(model);

                if (images != null && images.Count > 0 && images.First() != null)
                {
                    advertisement.Images = new List<Image>(images.Count);
                    advertisement.Thumbnails = new List<Thumbnail>(images.Count);

                    foreach (var image in images)
                    {
                        if (image == null) continue;

                        MemoryStream stream = new MemoryStream();
                        image.InputStream.CopyTo(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        //create path to save images
                        var guid = Guid.NewGuid().ToString();
                        var folder1 = guid.Substring(0, 2);
                        var folder2 = guid.Substring(2, 2);
                        var fileName = guid.Substring(4, 4) + ".jpg";

                        //create directory if not exists
                        var absolutePathImage = Server.MapPath($"{Constants.ImagesFolder + folder1}/{folder2}/");
                        if (!Directory.Exists(absolutePathImage)) Directory.CreateDirectory(absolutePathImage);
                        absolutePathImage = absolutePathImage + fileName;
                        var virtualPathImage = $"{folder1}/{folder2}/{fileName}";

                        var absolutePathThumbnail = Server.MapPath($"{Constants.ThumbnailsFolder + folder1}/{folder2}/");
                        if (!Directory.Exists(absolutePathThumbnail)) Directory.CreateDirectory(absolutePathThumbnail);
                        absolutePathThumbnail = absolutePathThumbnail + fileName;
                        var virtualPathThumbnail = $"{folder1}/{folder2}/{fileName}";

                        //convert and save image
                        ImageJob job = new ImageJob(stream, absolutePathImage, new Instructions("width=800;height=500;mode=pad;format=jpg;scale=both"));
                        job.Build();
                        Image newImage = new Image()
                        {
                            Path = virtualPathImage,
                            AdvertisementId = advertisement.Id
                        };
                        advertisement.Images.Add(newImage);

                        //ImageJob close stream when read, so create new
                        stream = new MemoryStream();
                        image.InputStream.Seek(0, SeekOrigin.Begin);
                        image.InputStream.CopyTo(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        //convert and save thumbnail
                        job = new ImageJob(stream, absolutePathThumbnail, new Instructions("width=135;height=100;mode=crop;format=jpg;scale=both"));
                        job.Build();
                        Thumbnail newThumbnail = new Thumbnail()
                        {
                            Path = virtualPathImage,
                            AdvertisementId = advertisement.Id
                        };
                        advertisement.Thumbnails.Add(newThumbnail);
                    }
                }

                if (model.WithParameters)
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CreateAutoView, AutoParameters>();
                    });
                    mapper = config.CreateMapper();
                    advertisement.AutoParameters = mapper.Map<AutoParameters>(model);
                }

                manager.AddAdvertisement(advertisement);

                //send notification to telegram
                var urlToCheck = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + Url.Action("Advertisements", "Check");
                TelegramApi api = new TelegramApi();
                api.SendNotification("*Новое объявление*\r\n[Перейти](" + urlToCheck + ")");

                return RedirectToAction("Success", "Home");
            }

            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var advertisement = manager.GetAdvertisement(id);
            if (advertisement == null) return HttpNotFound();

            bool isParametersExist = advertisement.AutoParameters != null;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advertisement, DetailAutoAdvView>()
                .ForMember(d => d.ProductionYear, opt => opt.MapFrom(s => s.AutoParameters.ProductionYear))
                .ForMember(d => d.KmAge, opt => opt.MapFrom(s => s.AutoParameters.KmAge))
                .ForMember(d => d.EngineCapacity, opt => opt.MapFrom(s => s.AutoParameters.EngineCapacity))
                .ForMember(d => d.EnginePower, opt => opt.MapFrom(s => s.AutoParameters.EnginePower))
                .ForMember(d => d.Color, opt => opt.MapFrom(s => s.AutoParameters.Color))
                .ForMember(d => d.MarkName, opt => opt.MapFrom(s => s.AutoParameters.MarkName))
                .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.AutoParameters.ModelName))
                .ForMember(d => d.CarcaseType, opt => opt.MapFrom(s => s.AutoParameters.CarcaseType.GetDisplayName()))
                .ForMember(d => d.DoorsNumber, opt => opt.MapFrom(s => s.AutoParameters.DoorsNumber.GetDisplayName()))
                .ForMember(d => d.EngineType, opt => opt.MapFrom(s => s.AutoParameters.EngineType.GetDisplayName()))
                .ForMember(d => d.TransmissionType, opt => opt.MapFrom(s => s.AutoParameters.TransmissionType.GetDisplayName()))
                .ForMember(d => d.DriveType, opt => opt.MapFrom(s => s.AutoParameters.DriveType.GetDisplayName()))
                .ForMember(d => d.SteeringWheelType, opt => opt.MapFrom(s => s.AutoParameters.SteeringWheelType.GetDisplayName()))
                .ForMember(d => d.State, opt => opt.MapFrom(s => s.AutoParameters.State.GetDisplayName()))
                .ForMember(d => d.IsParametersExist, opt => opt.UseValue(isParametersExist))
                .AfterMap((s, d) =>
                {
                    s.Images.OrderBy(i => i.Created);
                    foreach (var image in s.Images)
                        d.ImagesId.Add(image.Id);

                    if (!String.IsNullOrEmpty(d.PhoneLink))
                        d.PhoneLink = "tel:" + s.Phone;
                    if (!String.IsNullOrWhiteSpace(d.Description))
                        d.Description = d.Description.Replace("\r\n", "<br />");
                });
            });

            IMapper mapper = config.CreateMapper();
            var model = mapper.Map<DetailAutoAdvView>(advertisement);
            return View(model);
        }

        public ActionResult GetThumbnailByAdvId(Guid id)
        {
            var thumbnail = manager.GetThumbnailByAdvId(id);
            if (thumbnail == null)
                return File(Server.MapPath(Constants.NoThumbnailPath), "image/png");
            else
            {
                return File(Server.MapPath(Constants.ThumbnailsFolder + thumbnail.Path), "image/jpg");
            }
        }

        public ActionResult GetImage(Guid id)
        {
            if (id == Guid.Empty)
                return File(Server.MapPath(Constants.NoImagePath), "image/jpg");
            else
            {
                var image = manager.GetImageById(id);
                return File(Constants.ImagesFolder + image.Path, "image/jpg");
            }
        }
    }
}