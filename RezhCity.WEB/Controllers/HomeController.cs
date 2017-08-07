using RezhCity.DAL.Interfaces;
using RezhCity.DAL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RezhCity.DAL.Entities;
using RezhCity.WEB.Models;
using RezhCity.DAL.Enums;
using RezhCity.DAL.Utils;
using RezhCity.WEB.Utils;
using System.Threading.Tasks;

namespace RezhCity.WEB.Controllers
{
    public class HomeController : Controller
    {
        DataManager manager;
        public HomeController(DataManager _manager)
        {
            manager = _manager;
        }

        public ActionResult Index(int page = 1, string keyword = null)
        {
            var query = String.IsNullOrEmpty(keyword) ?
                manager.GetAdvertisementsQuery() :
                manager.SearchAdvQuery(keyword: keyword);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advertisement, ShortAdvView>()
                .AfterMap((s, d) =>
                {
                    d.Created = "размещено " + s.Created.ToString("dd-MM-yyyy HH:mm");
                    if (!String.IsNullOrEmpty(d.PhoneLink))
                        d.PhoneLink = "tel:" + s.Phone;
                    d.Price = s.Price.HasValue ? s.Price.ToString() : String.Empty;
                    switch (s.Category)
                    {
                        case Category.Auto: d.Url = Url.Action("Details", "Auto", new { id = s.Id }); break;
                        case Category.Clothes: d.Url = Url.Action("Details", "Clothes", new { id = s.Id }); break;
                        case Category.Electronic: d.Url = Url.Action("Details", "Electronic", new { id = s.Id }); break;
                        case Category.Furniture: d.Url = Url.Action("Details", "Furniture", new { id = s.Id }); break;
                        case Category.Other: d.Url = Url.Action("Details", "Other", new { id = s.Id }); break;
                        case Category.Realty: d.Url = Url.Action("Details", "Realty", new { id = s.Id }); break;
                    }
                });
            });

            var pagesCount = manager.GetPagesCount(query);
            query = manager.SetPagingForQuery(query, page);

            IMapper mapper = config.CreateMapper();
            IndexView model = new IndexView()
            {
                Advertisements = mapper.Map<List<ShortAdvView>>(query.ToList()),
                CurrentPage = page,
                PagesCount = pagesCount,
                Keyword = keyword,
                Category = this.ControllerContext.RouteData.Values["controller"].ToString()
            };
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

        public ActionResult Bus()
        {
            return View(new OutcityBusView());
        }

        public ActionResult Success()
        {
            string message = String.Empty;
            if (HttpContext.Request.UrlReferrer.Segments[1].Contains("Vacancy"))
                message = "Вакансия будет размещена после того, как пройдет проверку :)";
            else if (HttpContext.Request.UrlReferrer.Segments[1].Contains("Resume"))
                message = "Резюме будет размещено после того как, пройдет проверку :)";
            else
                message = "Объявление будет размещено после того как, пройдет проверку :)";

            ViewBag.Message = message;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            manager.Dispose();
            base.Dispose(disposing);
        }
    }
}