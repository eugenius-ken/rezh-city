using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class ShortResumeView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string VkId { get; set; }
        public string OkId { get; set; }
        public string Category { get; set; }
        public string Position { get; set; }
        public string Created { get; set; }
        public string Phone { get; set; }
        public string PhoneLink { get; set; }
        public string Url { get; set; }
    }
}