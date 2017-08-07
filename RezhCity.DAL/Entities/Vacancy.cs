using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class Vacancy
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string OwnerName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Company { get; set; }

        [Required]
        [MaxLength(40)]
        public string Phone { get; set; }

        [MaxLength(40)]
        public string Email { get; set; }

        [Required]
        [MaxLength(40)]
        public string Position { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Duties { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Requirements { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Conditions { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public VacancyCategory? Category { get; set; }

        public WorkShedule Shedule { get; set; }

        public WorkExperience? Experience { get; set; }
        public Education? Education { get; set; }

        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public bool Premoderated { get; set; }
    }
}
