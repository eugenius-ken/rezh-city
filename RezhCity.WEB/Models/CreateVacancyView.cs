using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateVacancyView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести название компании :(")]
        [StringLength(100, ErrorMessage = "Слишком уж длинное название, давай что-нибудь покороче :)")]
        [DisplayName("Компания*")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести своё имя :(")]
        [StringLength(40, ErrorMessage = "Слишком уж длинное у тебя имя, давай что-нибудь покороче :)")]
        [DisplayName("Контактное лицо*")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести телефон :(")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный номер телефона, давай что-нибудь покороче :)")]
        [DisplayName("Телефон*")]
        public string Phone { get; set; }

        [StringLength(40, ErrorMessage = "Слишком уж длинный e-mail, давай что-нибудь покороче :)")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Некорректный e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести должность :(")]
        [StringLength(40, ErrorMessage = "Слишком уж длинная должность, давай что-нибудь покороче :)")]
        [DisplayName("Должность*")]
        public string Position { get; set; }

        [DisplayName("Категория")]
        public VacancyCategory? Category { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл обязанности :(")]
        [StringLength(1000, ErrorMessage = "Слишком уж много написано, давай что-нибудь покороче :)")]
        [DisplayName("Обязанности кандидата*")]
        public string Duties { get; set; }

        [DisplayName("График работы")]
        public WorkShedule Shedule { get; set; }

        [DisplayName("Зарплата в месяц")]
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл требования :(")]
        [StringLength(1000, ErrorMessage = "Слишком уж много написано, давай что-нибудь покороче :)")]
        [DisplayName("Требования к кандидату*")]
        public string Requirements { get; set; }

        [DisplayName("Опыт работы")]
        public WorkExperience? Experience { get; set; }

        [DisplayName("Образование")]
        public Education? Education { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести условия работы :(")]
        [StringLength(1000, ErrorMessage = "Слишком уж много написано, давай что-нибудь покороче :)")]
        [DisplayName("Условия работы*")]
        public string Conditions { get; set; }

        [StringLength(100, ErrorMessage = "Слишком уж длинный адрес, давай что-нибудь покороче :)")]
        [DisplayName("Адрес")]
        public string Address { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [StringLength(1000, ErrorMessage = "Слишком уж много написано, давай что-нибудь покороче :)")]
        [DisplayName("Доп. информация")]
        public string Description { get; set; }
    }
}