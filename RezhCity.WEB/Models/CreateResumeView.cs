using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateResumeView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Кажется кто-то забыл заполнить ФИО :(")]
        [StringLength(40, ErrorMessage = "Слишком уж длинное у тебя имя, давай что-нибудь покороче :)")]
        [DisplayName("ФИО*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести телефон :(")]
        [DisplayName("Телефон*")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный номер телефона, давай что-нибудь покороче :)")]
        public string Phone { get; set; }

        [StringLength(1000, ErrorMessage = "Ну уж слишком много понаписано, давай что-нибудь покороче :)")]
        [DisplayName("О себе")]
        public string Description { get; set; }

        [StringLength(40, ErrorMessage = "Слишком уж длинная должность, давай что-нибудь покороче :)")]
        [DisplayName("Желаемая должность")]
        public string Position { get; set; }

        [DisplayName("ID ВКонтакте")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный ID ВКонтакте, давай что-нибудь покороче :)")]
        public string VkId { get; set; }

        [DisplayName("ID Одноклассники")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный ID Одноклассников, давай что-нибудь покороче :)")]
        public string OkId { get; set; }

        [DisplayName("Возраст")]
        [Range(0, 100, ErrorMessage = "Возраст должен быть в диапазоне 0-100")]
        public int? Age { get; set; }

        [DisplayName("Категория")]
        public VacancyCategory? Category { get; set; }

        [DisplayName("Желаемый график")]
        public WorkShedule Shedule { get; set; }
        
        [DisplayName("Опыт работы")]
        public WorkExperience Experience { get; set; }

        [DisplayName("Образование")]
        public Education Education { get; set; }
    }
}