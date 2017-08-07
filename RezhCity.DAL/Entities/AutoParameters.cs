using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class AutoParameters
    {
        [Key]
        [ForeignKey("Advertisement")]
        public Guid Id { get; set; }

        public int? ProductionYear { get; set; }
        public int? KmAge { get; set; }

        public State? State { get; set; }
        public CarcaseType? CarcaseType { get; set; }
        public DoorsNumber? DoorsNumber { get; set; }
        public EngineType? EngineType { get; set; }
        public TransmissionType? TransmissionType { get; set; }
        public DriveType? DriveType { get; set; }
        public SteeringWheelType? SteeringWheelType { get; set; }

        [MaxLength(40)]
        public string Color { get; set; }
        [MaxLength(40)]
        public string MarkName { get; set; }
        [MaxLength(40)]
        public string ModelName { get; set; }
        [MaxLength(10)]
        public string EngineCapacity { get; set; }
        public int? EnginePower { get; set; }
        
        public Advertisement Advertisement { get; set; }
    }
}
