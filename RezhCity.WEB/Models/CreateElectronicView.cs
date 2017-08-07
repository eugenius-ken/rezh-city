using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateElectronicView
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

        [Required(ErrorMessage = "Кажется, кто-то забыл заполнить название объявления :(")]
        [DisplayName("Название объявления*")]
        [StringLength(40, ErrorMessage = "Слишком уж длинное название, давай что-нибудь покороче :)")]
        public string Name { get; set; }

        [DisplayName("Дополнительная информация")]
        [StringLength(1000, ErrorMessage = "Ну уж слишком много понаписано, давай что-нибудь покороче :)")]
        public string Description { get; set; }

        [DisplayName("Цена, руб.")]
        public int? Price { get; set; }

        [DisplayName("Я хочу")]
        public AdvType Type { get; set; } = AdvType.Sell;
    }
}