using AutoMapper;
using RezhCity.DAL.Entities;
using RezhCity.DAL.Enums;
using RezhCity.DAL.Managers;
using RezhCity.DAL.Utils;
using RezhCity.WEB.Models;
using RezhCity.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RezhCity.WEB.Controllers
{
    public class VacancyController : Controller
    {
        DataManager manager;
        public VacancyController(DataManager _manager)
        {
            manager = _manager;
        }

        public ActionResult Index(int page = 1, VacancyCategory? type = null, string keyword = null)
        {
            var query = type.HasValue || !String.IsNullOrEmpty(keyword) ?
                manager.SearchVacancyQuery(category: type, keyword: keyword) :
                manager.GetVacancies();
            

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vacancy, ShortVacancyView>()
                .ForMember(dest => dest.Category, act => act.MapFrom(_ => _.Category.GetDisplayName()))
                .AfterMap((s, d) =>
                {
                    d.Created = "размещено " + s.Created.ToString("dd-MM-yyyy HH:mm");
                    d.PhoneLink = "tel:" + s.Phone.Remove(2, 1).Remove(5, 1);
                    d.Url = Url.Action("Details", "Vacancy", new { id = s.Id });
                    d.Salary = GetSalary(s.MinSalary, s.MaxSalary);
                });
            });

            var pagesCount = manager.GetPagesCount(query);
            query = manager.SetPagingForQuery(query, page);

            IMapper mapper = config.CreateMapper();
            var model = new IndexVacancyView()
            {
                Vacancies = mapper.Map<List<ShortVacancyView>>(query.ToList()),
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVacancyView model)
        {
            if (model.Phone.Last() == '_')
                ModelState.AddModelError("Phone", "Кажется, Вы неверно ввели телефон :(");

            if (ModelState.IsValid)
            {
                //mapping
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateVacancyView, Vacancy>()
                    .ForMember(dest => dest.Id, opt => opt.UseValue(Guid.NewGuid()))
                    .ForMember(dest => dest.Premoderated, opt => opt.UseValue(false))
                    .ForMember(dest => dest.Created, opt => opt.UseValue(DateTime.Now));
                });
                IMapper mapper = config.CreateMapper();
                Vacancy vacancy = mapper.Map<Vacancy>(model);
                manager.AddVacancy(vacancy);

                //send notification to telegram
                var urlToCheck = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + Url.Action("Vacancies", "Check");
                TelegramApi api = new TelegramApi();
                api.SendNotification("*Новая вакансия*\r\n[Перейти](" + urlToCheck + ")");

                return RedirectToAction("Success", "Home");
            }
            else
                return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var vacancy = manager.GetVacancy(id);
            if (vacancy == null)
                return HttpNotFound();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vacancy, DetailVacancyView>()
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
                    if(!String.IsNullOrEmpty(d.Description))
                        d.Description = d.Description.Replace("\r\n", "<br />");
                });
            });

            IMapper mapper = config.CreateMapper();
            var model = mapper.Map<DetailVacancyView>(vacancy);

            return View(model);
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