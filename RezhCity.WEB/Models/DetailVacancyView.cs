using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class DetailVacancyView
    {
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string PhoneLink { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Duties { get; set; }
        public string Requirements { get; set; }
        public string Conditions { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Shedule { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Salary { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Created { get; set; }
    }
}