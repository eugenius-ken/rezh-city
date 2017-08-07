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
    public class IndexRealtyView : ISearchable
    {
        public List<ShortAdvView> Advertisements { get; set; }

        public AdvType? Type { get; set; }
        public AdvObject? Obj { get; set; }
        public string Keyword { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem() { Value = ((int)RealtyTradeType.Sell).ToString(), Text = "Продать" },
            new SelectListItem() { Value = ((int)RealtyTradeType.Buy).ToString(), Text = "Купить" },
            new SelectListItem() { Value = ((int)RealtyTradeType.RentOff).ToString(), Text = "Сдать" },
            new SelectListItem() { Value = ((int)RealtyTradeType.RentOn).ToString(), Text = "Снять" },
            new SelectListItem() { Value = ((int)RealtyTradeType.Exchange).ToString(), Text = "Обменять" }
        };

        public IEnumerable<SelectListItem> Objects { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem() { Value = ((int)AdvObject.Appartments).ToString(), Text = AdvObject.Appartments.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.Room).ToString(), Text = AdvObject.Room.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.House).ToString(), Text = AdvObject.House.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.Garage).ToString(), Text = AdvObject.Garage.GetDisplayName() },
            new SelectListItem() { Value = ((int)AdvObject.Other).ToString(), Text = AdvObject.Other.GetDisplayName() }

        };

        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }

        public string Category { get; set; }
    }
}