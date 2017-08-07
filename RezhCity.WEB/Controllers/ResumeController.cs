using AutoMapper;
using ImageResizer;
using RezhCity.DAL.Entities;
using RezhCity.DAL.Enums;
using RezhCity.DAL.Managers;
using RezhCity.DAL.Utils;
using RezhCity.WEB.Models;
using RezhCity.WEB.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RezhCity.WEB.Controllers
{
    public class ResumeController : Controller
    {
        DataManager manager;
        public ResumeController(DataManager _manager)
        {
            manager = _manager;
        }

        public ActionResult Index(int page = 1, VacancyCategory? type = null, string keyword = null)
        {
            var query = type.HasValue || !String.IsNullOrEmpty(keyword) ?
                manager.SearchResumeQuery(category: type, keyword: keyword) :
                manager.GetResumes();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Resume, ShortResumeView>()
                .ForMember(dest => dest.Category, act => act.MapFrom(_ => _.Category.GetDisplayName()))
                .AfterMap((s, d) =>
                {
                    d.Created = "размещено " + s.Created.ToString("dd-MM-yyyy HH:mm");
                    d.PhoneLink = "tel:" + s.Phone.Remove(2, 1).Remove(5, 1);
                    d.Url = Url.Action("Details", "Resume", new { id = s.Id });
                    
                });
            });

            var pagesCount = manager.GetPagesCount(query);
            query = manager.SetPagingForQuery(query, page);

            IMapper mapper = config.CreateMapper();
            var model = new IndexResumeView()
            {
                Resumes = mapper.Map<List<ShortResumeView>>(query.ToList()),
                CurrentPage = page,
                PagesCount = pagesCount,
                Keyword = keyword,
                Category = this.ControllerContext.RouteData.Values["controller"].ToString()
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateResumeView model, List<CreateWorkplaceView> workplaces, List<CreateSchoolView> schools, HttpPostedFileBase file, HttpPostedFileBase photo)
        {
            if (model.Phone.Last() == '_')
                ModelState.AddModelError("Phone", "Кажется, Вы неверно ввели телефон :(");

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateResumeView, Resume>()
                    .ForMember(dest => dest.Id, opt => opt.UseValue(Guid.NewGuid()))
                    .ForMember(dest => dest.Created, opt => opt.UseValue(DateTime.Now))
                    .ForMember(dest => dest.Premoderated, opt => opt.UseValue(false))
                    .AfterMap((s, d) =>
                    {
                        d.VkId = String.IsNullOrWhiteSpace(s.VkId) ? null : "https://vk.com/" + s.VkId;
                        d.OkId = String.IsNullOrWhiteSpace(s.OkId) ? null : "https://ok.ru/profile/" + s.OkId;
                    });
                });

                IMapper mapper = config.CreateMapper();
                Resume resume = mapper.Map<Resume>(model);

                if (file != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        file.InputStream.CopyTo(stream);
                        resume.File = new DAL.Entities.File()
                        {
                            Name = file.FileName,
                            Value = stream.ToArray()
                        };
                    }
                }

                if (photo != null)
                {
                    resume.Images = new List<Image>(1);
                    resume.Thumbnails = new List<Thumbnail>(1);

                    MemoryStream stream = new MemoryStream();
                    photo.InputStream.CopyTo(stream);
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
                        ResumeId = resume.Id
                    };
                    resume.Images.Add(newImage);

                    //ImageJob close stream when read, so create new
                    stream = new MemoryStream();
                    photo.InputStream.Seek(0, SeekOrigin.Begin);
                    photo.InputStream.CopyTo(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    //convert and save thumbnail
                    job = new ImageJob(stream, absolutePathThumbnail, new Instructions("width=135;height=100;mode=crop;format=jpg;scale=both"));
                    job.Build();
                    Thumbnail newThumbnail = new Thumbnail()
                    {
                        Path = virtualPathImage,
                        ResumeId = resume.Id
                    };
                    resume.Thumbnails.Add(newThumbnail);
                }

                if (workplaces != null)
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CreateWorkplaceView, Workplace>()
                        .AfterMap((s, d) =>
                        {
                            d.Id = Guid.NewGuid();
                        });
                    });

                    mapper = config.CreateMapper();

                    resume.Workplaces = mapper.Map<List<Workplace>>(workplaces);
                }

                if (schools != null)
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CreateSchoolView, School>()
                        .AfterMap((s, d) =>
                        {
                            d.Id = Guid.NewGuid();
                        });
                    });
                    mapper = config.CreateMapper();

                    resume.Schools = mapper.Map<ICollection<School>>(schools);
                }

                manager.AddResume(resume);

                //send notification to telegram
                var urlToCheck = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + Url.Action("Resumes", "Check");
                TelegramApi api = new TelegramApi();
                api.SendNotification("*Новое резюме*\r\n[Перейти](" + urlToCheck + ")");

                return RedirectToAction("Success", "Home");
            }
            else
                return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var resume = manager.GetResume(id);
            if (resume == null)
                return HttpNotFound();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Resume, DetailResumeView>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(_ => _.Category.GetDisplayName()))
                .ForMember(dest => dest.Shedule, opt => opt.MapFrom(_ => _.Shedule.GetDisplayName()))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(_ => _.Experience.GetDisplayName()))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(_ => _.Education.GetDisplayName()))
                .ForMember(dest => dest.Workplaces, opt => opt.Ignore())
                .ForMember(dest => dest.Schools, opt => opt.Ignore())
                .AfterMap((s, d) =>
                {
                    if (!String.IsNullOrWhiteSpace(d.Description))
                        d.Description = d.Description.Replace("\r\n", "<br />");
                    d.PhoneLink = "tel:" + s.Phone.Remove(2, 1).Remove(5, 1);
                    if (s.File != null)
                        d.FileId = s.File.Id;

                    if(s.Images != null && s.Images.Count > 0)
                        d.PhotoId = s.Images.First().Id;
                });
            });

            IMapper mapper = config.CreateMapper();
            var model = mapper.Map<DetailResumeView>(resume);

            if (resume.Workplaces != null)
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Workplace, DetailWorkplaceView>()
                    .ForMember(dest => dest.StartMonth, opt => opt.MapFrom(_ => _.StartMonth.GetDisplayName()))
                    .ForMember(dest => dest.FinishMonth, opt => opt.MapFrom(_ => _.FinishMonth.GetDisplayName()));
                });

                mapper = config.CreateMapper();
                model.Workplaces = mapper.Map<List<DetailWorkplaceView>>(resume.Workplaces);
            }

            if (resume.Schools != null)
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<School, DetailSchoolView>();
                });

                mapper = config.CreateMapper();
                model.Schools = mapper.Map<List<DetailSchoolView>>(resume.Schools);
            }

            return View(model);
        }

        //index is needed for list's binding when post
        public ActionResult GetWorkplaceView(int index)
        {
            return PartialView("~/Views/Partial/_Workplace.cshtml", new CreateWorkplaceView(index));
        }

        //index is needed for list's binding when post
        public ActionResult GetSchoolView(int index)
        {
            return PartialView("~/Views/Partial/_School.cshtml", new CreateSchoolView(index));
        }

        public ActionResult GetThumbnailByResumeId(Guid id)
        {
            var thumbnail = manager.GetThumbnailByResumeId(id);
            if (thumbnail == null)
                return File(Server.MapPath(Constants.NoThumbnailPath), "image/png");
            else
            {
                return File(Server.MapPath(Constants.ThumbnailsFolder + thumbnail.Path), "image/jpg");
            }
        }

        public ActionResult GetPhoto(Guid id)
        {
            var photo = manager.GetImageById(id);
            if(photo == null)
                return File(Server.MapPath(Constants.NoImagePath), "image/png");
            else
            {
                return File(Server.MapPath(Constants.ImagesFolder + photo.Path), "image/jpg");
            }
        }

        public ActionResult Download(Guid id)
        {
            var file = manager.GetFile(id);
            if (file == null)
                return HttpNotFound();

            return File(file.Value, "application/msword", file.Name);
        }
    }
}