using RezhCity.DAL.Enums;
using RezhCity.WEB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class IndexResumeView : ISearchable
    {
        public IList<ShortResumeView> Resumes { get; set; }

        public string Keyword { get; set; }
        public string Category { get; set; } //ControllerName

        public VacancyCategory? Type { get; set; }

        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
    }
}