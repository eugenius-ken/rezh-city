using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class Workplace
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(40)]
        public string Position { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

        public Months? StartMonth { get; set; }
        public int? StartYear { get; set; }

        public Months? FinishMonth { get; set; }
        public int? FinishYear { get; set; }

        public Guid ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
