using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class Resume
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(40)]
        public string Phone { get; set; }
        [MaxLength(40)]
        public string Position { get; set; }
        [MaxLength(40)]
        public string VkId { get; set; }
        [MaxLength(40)]
        public string OkId { get; set; }


        public int? Age { get; set; }

        public VacancyCategory? Category { get; set; }
        public WorkShedule Shedule { get; set; }
        public WorkExperience Experience { get; set; }
        public Education Education { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public bool Premoderated { get; set; }
        public virtual ICollection<Workplace> Workplaces { get; set; }
        public virtual ICollection<School> Schools { get; set; }

        public virtual File File { get; set; }
        public virtual ICollection<Image> Images { get; set; } //really save 1 photo
        public virtual ICollection<Thumbnail> Thumbnails { get; set; } //really save 1 photo
    }
}
