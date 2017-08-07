using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class DetailWorkplaceView
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string StartMonth { get; set; }
        public int? StartYear { get; set; }
        public string FinishMonth { get; set; }
        public string FinishYear { get; set; }
    }
}