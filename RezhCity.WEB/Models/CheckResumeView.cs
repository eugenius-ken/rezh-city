using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CheckResumeView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PhoneLink { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string VkId { get; set; }
        public string OkId { get; set; }
        public int? Age { get; set; }
        public string Category { get; set; }
        public string Shedule { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }

        public Guid? FileId { get; set; }

        public Guid? PhotoId { get; set; }

        public List<DetailWorkplaceView> Workplaces { get; set; } = new List<DetailWorkplaceView>();
        public List<DetailSchoolView> Schools { get; set; } = new List<DetailSchoolView>();

        public bool IsResumesExist { get; set; } = true;

        public int Count { get; set; }
    }
}