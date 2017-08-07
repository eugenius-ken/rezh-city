using AutoMapper;
using RezhCity.DAL.Entities;
using RezhCity.DAL.Managers;
using RezhCity.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezhCity.DAL.Utils;
using System.Threading.Tasks;
using RezhCity.WEB.Utils;

namespace RezhCity.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CheckController : Controller
    {
        DataManager manager;
        public CheckController(DataManager _manager)
        {
            manager = _manager;
        }

        #region Advertisements
        public ActionResult Advertisements(Guid? id = null)
        {
            var advertisement = id.HasValue ? manager.GetAdvertisement(id.Value) : manager.GetAdvertisementForPremoderation();
            CheckAdvertisementView model;

            if (advertisement == null)
            {
                model = new CheckAdvertisementView() { IsAdvertisementsExist = false };
            }
            else
            {
                MapperConfiguration config;
                if (advertisement.AutoParameters != null)
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Advertisement, CheckAdvertisementView>()
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
                        .AfterMap((s, d) =>
                        {
                            s.Images.OrderBy(i => i.Created);
                            foreach (var image in s.Images)
                                d.ImagesId.Add(image.Id);

                            if (!String.IsNullOrEmpty(s.Phone))
                                d.PhoneLink = "tel:" + s.Phone;
                            if (!String.IsNullOrWhiteSpace(d.Description))
                                d.Description = d.Description.Replace("\r\n", "<br />");
                        });
                    });
                    IMapper mapper = config.CreateMapper();
                    model = mapper.Map<CheckAdvertisementView>(advertisement);
                    model.IsAutoParametersExist = true;
                }
                else if (advertisement.RealtyParameters != null)
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Advertisement, CheckAdvertisementView>()
                        .ForMember(d => d.Address, opt => opt.MapFrom(s => s.RealtyParameters.Address))
                        .ForMember(d => d.Square, opt => opt.MapFrom(s => s.RealtyParameters.Square))
                        .ForMember(d => d.FloorNumber, opt => opt.MapFrom(s => s.RealtyParameters.FloorNumber.GetDisplayName()))
                        .ForMember(d => d.SummaryFloor, opt => opt.MapFrom(s => s.RealtyParameters.SummaryFloor.GetDisplayName()))
                        .ForMember(d => d.RoomNumber, opt => opt.MapFrom(s => s.RealtyParameters.RoomNumber.GetDisplayName()))
                        .ForMember(d => d.Latitude, opt => opt.MapFrom(s => s.RealtyParameters.Latitude))
                        .ForMember(d => d.Longitude, opt => opt.MapFrom(s => s.RealtyParameters.Longitude))
                        .AfterMap((s, d) =>
                        {
                            s.Images.OrderBy(i => i.Created);
                            foreach (var image in s.Images)
                                d.ImagesId.Add(image.Id);

                            if (!String.IsNullOrEmpty(s.Phone))
                                d.PhoneLink = "tel:" + s.Phone;
                            if (!String.IsNullOrWhiteSpace(d.Description))
                                d.Description = d.Description.Replace("\r\n", "<br />");
                        });
                    });
                    IMapper mapper = config.CreateMapper();
                    model = mapper.Map<CheckAdvertisementView>(advertisement);
                    model.IsRealtyParametersExist = true;
                }
                else
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Advertisement, CheckAdvertisementView>()
                        .AfterMap((s, d) =>
                        {
                            if (!String.IsNullOrEmpty(s.Phone))
                                d.PhoneLink = "tel:" + s.Phone;
                            s.Images.OrderBy(i => i.Created);
                            foreach (var image in s.Images)
                            {
                                d.ImagesId.Add(image.Id);
                            }
                            if (!String.IsNullOrWhiteSpace(d.Description))
                                d.Description = d.Description.Replace("\r\n", "<br />");
                        });
                    });
                    IMapper mapper = config.CreateMapper();
                    model = mapper.Map<CheckAdvertisementView>(advertisement);
                }

                model.Count = manager.GetCountNotPremoderatedAdvertisements();
            }

            return View(model);
        }

        public ActionResult EditAdvertisement(Guid id)
        {
            var advertisement = manager.GetAdvertisement(id);
            if (advertisement == null) return HttpNotFound();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advertisement, EditAdvertisementView>();
            });
            IMapper mapper = config.CreateMapper();
            var model = mapper.Map<EditAdvertisementView>(advertisement);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdvertisement(EditAdvertisementView model)
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
                var advertisement = manager.GetAdvertisement(model.Id);
                if (advertisement == null) return HttpNotFound();

                advertisement.OwnerName = model.OwnerName;
                advertisement.Phone = model.Phone;
                advertisement.VkId = model.VkId;
                advertisement.OkId = model.OkId;
                advertisement.Name = model.Name;
                advertisement.Description = model.Description;
                advertisement.Price = model.Price;

                manager.EditAdvertisement(advertisement);

                return RedirectToAction("Advertisements", new { id = model.Id });
            }

            return View(model);
        }

        public ActionResult BanAdvertisement(Guid id)
        {
            manager.DeleteAdvertisement(id);
            return RedirectToAction("Advertisements");
        }

        public async Task<ActionResult> AllowAdvertisement(Guid id)
        {
            var advertisement = manager.GetAdvertisement(id);
            if (advertisement == null)
                return HttpNotFound();

            advertisement.Premoderated = true;
            manager.EditAdvertisement(advertisement);

            //need to change for production
            var urlToDetail = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + Url.Action("Details", advertisement.Category.ToString(), new { id = advertisement.Id });
            //var urlToDetail = "https://ya.ru";
            List<String> images = new List<String>(5);
            int i = 0;
            foreach (var image in advertisement.Images)
            {
                images.Add(Server.MapPath(Constants.ImagesFolder + image.Path));
                if (++i > 4) break; //max photo's count is 6 for VK Api (but last is a urlToDetail, so 5 photos)
            }

            VKApi vkApi = new VKApi();
            OKApi okApi = new OKApi();

            string text = advertisement.Name;
            if (!String.IsNullOrEmpty(advertisement.Description))
                text += "\r\n" + advertisement.Description;
            if (advertisement.Price.HasValue)
                text += "\r\nЦена: " + advertisement.Price.ToString() + " руб.";

            text += "\r\n\r\nКонтакты:\r\n" + advertisement.OwnerName;
            if (!String.IsNullOrEmpty(advertisement.Phone))
                text += "\r\n" + advertisement.Phone;
            if (!String.IsNullOrEmpty(advertisement.VkId))
                text += "\r\n" + advertisement.VkId;
            if (!String.IsNullOrEmpty(advertisement.OkId))
                text += "\r\n" + advertisement.OkId;

            await vkApi.Post(text, images, urlToDetail);
            await okApi.Post(text, images, urlToDetail);


            return RedirectToAction("Advertisements");
        }
        #endregion

        #region Vacancies
        public ActionResult Vacancies(Guid? id = null)
        {
            var vacancy = id.HasValue ? manager.GetVacancy(id.Value) : manager.GetVacancyForPremoderation();
            CheckVacancyView model;

            if (vacancy == null)
            {
                model = new CheckVacancyView() { IsVacanciesExist = false };
            }
            else
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Vacancy, CheckVacancyView>()
                    .ForMember(dest => dest.Shedule, opt => opt.MapFrom(_ => _.Shedule.GetDisplayName()))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(_ => _.Category.HasValue ? _.Category.GetDisplayName() : ""))
                    .ForMember(dest => dest.Experience, opt => opt.MapFrom(_ => _.Experience.HasValue ? _.Experience.GetDisplayName() : "Не имеет значения"))
                    .ForMember(dest => dest.Education, opt => opt.MapFrom(_ => _.Education.HasValue ? _.Education.GetDisplayName() : "Не имеет значения"))
                    .AfterMap((s, d) =>
                    {
                        d.Created = "размещено " + s.Created.ToString("dd-MM-yyyy HH:mm");
                        d.Salary = GetSalary(s.MinSalary, s.MaxSalary);
                        d.PhoneLink = "tel:" + s.Phone.Remove(2, 1).Remove(5, 1);
                        d.Duties = d.Duties.Replace("\r\n", "<br />");
                        d.Requirements = d.Requirements.Replace("\r\n", "<br />");
                        d.Conditions = d.Conditions.Replace("\r\n", "<br />");
                        if (!String.IsNullOrEmpty(d.Description))
                            d.Description = d.Description.Replace("\r\n", "<br />");
                    });
                });
                var mapper = config.CreateMapper();
                model = mapper.Map<CheckVacancyView>(vacancy);
                model.Count = manager.GetCountNotPremoderatedVacancies();
            }

            return View(model);
        }

        public ActionResult EditVacancy(Guid id)
        {
            var vacancy = manager.GetVacancy(id);
            if (vacancy == null) return HttpNotFound();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vacancy, EditVacancyView>();
            });
            IMapper mapper = config.CreateMapper();
            var model = mapper.Map<EditVacancyView>(vacancy);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVacancy(EditVacancyView model)
        {
            if (model.Phone.Last() == '_')
                ModelState.AddModelError("Phone", "Кажется, Вы неверно ввели телефон :(");

            if(ModelState.IsValid)
            {
                var vacancy = manager.GetVacancy(model.Id);
                if (vacancy == null) return HttpNotFound();
                
                vacancy.Company = model.Company;
                vacancy.OwnerName = model.OwnerName;
                vacancy.Phone = model.Phone;
                vacancy.Email = model.Email;
                vacancy.Position = model.Position;
                vacancy.Category = model.Category;
                vacancy.Duties = model.Duties;
                vacancy.Shedule = model.Shedule;
                vacancy.MinSalary = model.MinSalary;
                vacancy.MaxSalary = model.MaxSalary;
                vacancy.Requirements = model.Requirements;
                vacancy.Experience = model.Experience;
                vacancy.Education = model.Education;
                vacancy.Conditions = model.Conditions;
                vacancy.Address = model.Address;
                vacancy.Description = model.Description;

                manager.EditVacancy(vacancy);
                return RedirectToAction("Vacancies", new { id = model.Id });
            }

            return View(model);
        }

        public ActionResult BanVacancy(Guid id)
        {
            manager.DeleteVacancy(id);
            return RedirectToAction("Vacancies");
        }

        public ActionResult AllowVacancy(Guid id)
        {
            var vacancy = manager.GetVacancy(id);
            if (vacancy == null)
                return HttpNotFound();

            vacancy.Premoderated = true;
            manager.EditVacancy(vacancy);

            return RedirectToAction("Advertisements");
        }
        #endregion

        #region Resumes
        public ActionResult Resumes()
        {
            var resume = manager.GetResumeForPremoderation();
            CheckResumeView model;

            if (resume == null)
            {
                model = new CheckResumeView() { IsResumesExist = false };
            }
            else
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Resume, CheckResumeView>()
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

                        if (s.Images != null && s.Images.Count > 0)
                            d.PhotoId = s.Images.First().Id;
                    });
                });

                IMapper mapper = config.CreateMapper();
                model = mapper.Map<CheckResumeView>(resume);

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
                model.Count = manager.GetCountNotPremoderatedResume();
            }

            return View(model);
        }

        public ActionResult BanResume(Guid id)
        {
            manager.DeleteResume(id);
            return RedirectToAction("Resumes");
        }

        public ActionResult AllowResume(Guid id)
        {
            var resume = manager.GetResume(id);
            if (resume == null)
                return HttpNotFound();

            resume.Premoderated = true;
            manager.EditResume(resume);

            return RedirectToAction("Resumes");
        }
        #endregion

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

        public ActionResult GetPhoto(Guid id)
        {
            var photo = manager.GetImageById(id);
            if (photo == null)
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

        private string GetSalary(int? minSalary, int? maxSalary)
        {

            if (minSalary.HasValue && maxSalary.HasValue)
                return minSalary.ToString() + " - " + maxSalary.ToString() + " руб.";
            else if (minSalary.HasValue && !maxSalary.HasValue)
                return "от " + minSalary.ToString() + " руб.";
            else if (maxSalary.HasValue && !minSalary.HasValue)
                return "до " + maxSalary.ToString() + " руб.";

            return String.Empty;
        }
    }
}