using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class School
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Specialty { get; set; }
        public int? FinishYear { get; set; }

        public Guid ResumeId { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
