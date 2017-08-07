using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class ShortVacancyView
    {
        public Guid Id { get; set; }
        public string Company { get; set; }

        public string Phone { get; set; }
        public string PhoneLink { get; set; }
        public string Position { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public string Created { get; set; }

        public string Url { get; set; }
    }
}