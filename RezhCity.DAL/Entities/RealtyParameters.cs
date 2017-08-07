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
    public class RealtyParameters
    {
        [Key]
        [ForeignKey("Advertisement")]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public RoomNumber? RoomNumber { get; set; }

        public FloorNumber? FloorNumber { get; set; }

        public FloorNumber? SummaryFloor { get; set; }

        public double? Square { get; set; }

        public RealtyTradeType? TradeType { get; set; }

        public WallType? WallType { get; set; }

        public RealtyTradeType Type { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}
