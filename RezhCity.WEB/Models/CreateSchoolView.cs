using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateSchoolView
    {
        public Guid Id { get; set; }
        public int Index { get; set; }

        [Required(ErrorMessage = "Кажется кто-то забыл ввести название учебного заведения :(")]
        [StringLength(100, ErrorMessage = "Слишком длинное название, давай что-нибудь покороче :)")]
        [DisplayName("Название*")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Слишком длинная специальность, давай что-нибудь покороче :)")]
        [DisplayName("Специальность")]
        public string Specialty { get; set; }

        [DisplayName("Год окончания")]
        public int? FinishYear { get; set; }

        public List<int> Years { get; }

        public CreateSchoolView(int index)
        {
            Years = new List<int>();
            for (int i = DateTime.Now.Year; i > 1969; i--)
                Years.Add(i);

            this.Index = index;
        }

        public CreateSchoolView() { }
    }
}