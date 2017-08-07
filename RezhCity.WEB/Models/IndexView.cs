using RezhCity.WEB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class IndexView : ISearchable
    {
        public List<ShortAdvView> Advertisements { get; set; }

        public string Keyword { get; set; }

        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }

        public string Category { get; set; }
    }
}