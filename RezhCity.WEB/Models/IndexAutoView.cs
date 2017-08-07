using RezhCity.DAL.Enums;
using RezhCity.DAL.Utils;
using RezhCity.WEB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RezhCity.WEB.Models
{
    public class IndexAutoView : ISearchable
    {
        public List<ShortAdvView> Advertisements { get; set; }

        public AdvType? Type { get; set; }
        public AdvObject? Obj { get; set; }
        public string Keyword { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem() { Value = ((int)AdvType.Sell).ToString(), Text = "Продажа" },
            new SelectListItem() { Value = ((int)AdvType.Buy).ToString(), Text = "Покупка" },
            new SelectListItem() { Value = ((int)AdvType.Transfer).ToString(), Text = "Обмен" },
            new SelectListItem() { Value = ((int)AdvType.Give).ToString(), Text = "Даром" }
        };

        public IEnumerable<SelectListItem> Objects { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem() { Value = ((int)AdvObject.Auto).ToString(), Text = AdvObject.Auto.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.Moto).ToString(), Text = AdvObject.Moto.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.Spares).ToString(), Text = AdvObject.Spares.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.Other).ToString(), Text = AdvObject.Other.GetDisplayName() }
        };

        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }

        public string Category { get; set; }
    }
}