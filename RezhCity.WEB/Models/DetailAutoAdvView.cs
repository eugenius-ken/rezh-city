using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class DetailAutoAdvView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Price { get; set; }

        public DateTime Created { get; set; }

        public int? ProductionYear { get; set; }
        public int? KmAge { get; set; }

        public string State { get; set; }
        public string CarcaseType { get; set; }
        public string DoorsNumber { get; set; }
        public string EngineType { get; set; }
        public string TransmissionType { get; set; }
        public string DriveType { get; set; }
        public string SteeringWheelType { get; set; }

        public string EngineCapacity { get; set; }
        public int? EnginePower { get; set; }

        public string OwnerName { get; set; }
        public string Phone { get; set; }
        public string PhoneLink { get; set; }

        public string Color { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public string VkId { get; set; }
        public string OkId { get; set; }
        public IList<Guid> ImagesId { get; set; } = new List<Guid>();

        public bool IsParametersExist { get; set; } = false;
    }
}