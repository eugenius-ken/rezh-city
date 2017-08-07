using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateWorkplaceView
    {
        public Guid Id { get; set; }
        public int Index { get; set; }

        [Required(ErrorMessage = "Кажется кто-то забыл ввести название компании :(")]
        [StringLength(100, ErrorMessage = "Слишком длинное название компании, давай что-нибудь покороче :)")]
        [DisplayName("Компания*")]
        public string Name { get; set; }

        [StringLength(40, ErrorMessage = "Слишком длинное название должности, давай что-нибудь покороче :)")]
        [DisplayName("Должность")]
        public string Position { get; set; }

        [StringLength(1000, ErrorMessage = "Ну уж слишком много понаписано, давай что-нибудь покороче :)")]
        [DisplayName("Комментарий")]
        public string Description { get; set; }

        [DisplayName("Начало работы")]
        public Months? StartMonth { get; set; }
        public int? StartYear { get; set; }

        [DisplayName("Конец работы")]
        public Months? FinishMonth { get; set; }
        public int? FinishYear { get; set; }

        public List<int> Years { get; }

        public CreateWorkplaceView(int index)
        {
            Years = new List<int>();
            for(int i = DateTime.Now.Year; i > 1969; i--)
                Years.Add(i);

            this.Index = index;
        }

        public CreateWorkplaceView() { }
    }
}