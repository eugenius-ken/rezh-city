using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateRealtyView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Кажется, кто-то забыл ввести своё имя :(")]
        [DisplayName("Ваше имя*")]
        [StringLength(40, ErrorMessage = "Слишком уж длинное у тебя имя, давай что-нибудь покороче :)")]
        public string OwnerName { get; set; }

        [DisplayName("Телефон")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный номер телефона, давай что-нибудь покороче :)")]
        public string Phone { get; set; }

        [DisplayName("ID ВКонтакте")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный ID ВКонтакте, давай что-нибудь покороче :)")]
        public string VkId { get; set; }

        [DisplayName("ID Одноклассники")]
        [StringLength(40, ErrorMessage = "Слишком уж длинный ID Одноклассников, давай что-нибудь покороче :)")]
        public string OkId { get; set; }

        [Required(ErrorMessage = "Введите название объявления")]
        [DisplayName("Название объявления*")]
        [StringLength(40, ErrorMessage = "Слишком уж длинное название, давай что-нибудь покороче :)")]
        public string Name { get; set; }

        [DisplayName("Дополнительная информация")]
        [StringLength(1000, ErrorMessage = "Ну уж слишком много понаписано, давай что-нибудь покороче :)")]
        public string Description { get; set; }

        [DisplayName("Цена, руб.")]
        public int? Price { get; set; }

        [DisplayName("Адрес")]
        [StringLength(100, ErrorMessage = "Ну уж слишком длинный адрес, давай что-нибудь покороче :)")]
        public string Address { get; set; }

        [Range(0, 1000, ErrorMessage = "Что-то не так с площадью, введи число от 0 до 1000")]
        public double? Square { get; set; } 

        [DisplayName("Что продаете")]
        public AdvObject? Object { get; set; }

        [DisplayName("Этаж")]
        public FloorNumber? FloorNumber { get; set; }

        [DisplayName("Всего этажей")]
        public FloorNumber? SummaryFloor { get; set; }

        [DisplayName("Количество комнат")]
        public RoomNumber? RoomNumber { get; set; }

        [DisplayName("Я хочу")]
        public RealtyTradeType Type { get; set; } = RealtyTradeType.Sell;

        public bool WithParameters { get; set; } = false;

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

    }
}