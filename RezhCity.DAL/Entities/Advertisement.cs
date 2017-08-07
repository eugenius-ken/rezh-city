using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class Advertisement
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(40)]
        public string OwnerName { get; set; }

        [MaxLength(40)]
        public string Phone { get; set; }

        public int? Price { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public AdvType Type { get; set; }

        [Required]
        public Category Category { get; set; }

        public AdvObject? Object { get; set; }

        [MaxLength(100)]
        public string VkId { get; set; }
        [MaxLength(100)]
        public string OkId { get; set; }

        public bool Premoderated { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Thumbnail> Thumbnails { get; set; }

        public virtual AutoParameters AutoParameters { get; set; }
        public virtual RealtyParameters RealtyParameters { get; set; }
    }
}
