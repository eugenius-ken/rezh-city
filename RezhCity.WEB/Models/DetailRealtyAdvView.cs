using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class DetailRealtyAdvView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Price { get; set; }

        public DateTime Created { get; set; }

        public string OwnerName { get; set; }
        public string Phone { get; set; }
        public string PhoneLink { get; set; }

        public string VkId { get; set; }
        public string OkId { get; set; }
        public IList<Guid> ImagesId { get; set; } = new List<Guid>();

        public string Address { get; set; }
        public string FloorNumber { get; set; }
        public string SummaryFloor { get; set; }
        public string RoomNumber { get; set; }
        public double? Square { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool IsParametersExist { get; set; } = false;
    }
}