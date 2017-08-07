using RezhCity.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class CreateAutoView
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

        [DisplayName("Цвет")]
        [StringLength(40, ErrorMessage = "Не бывает цвета с таким названием, давай другой :)")]
        public string Color { get; set; }

        [DisplayName("Марка")]
        [StringLength(40, ErrorMessage = "Не бывает такой длинной марки машины, давай что-нибудь покороче :)")]
        public string MarkName { get; set; }

        [DisplayName("Модель")]
        [StringLength(40, ErrorMessage = "Не бывает такой длинной модели машины, давай что-нибудь покороче :)")]
        public string ModelName { get; set; }

        [DisplayName("Цена, руб.")]
        public int? Price { get; set; }

        [DisplayName("Год выпуска")]
        public int? ProductionYear { get; set; }

        public List<Int32> Years { get; set; }

        [DisplayName("Пробег, км.")]
        public int? KmAge { get; set; }

        [DisplayName("Мощность двигателя, л.с.")]
        public int? EnginePower { get; set; }

        [StringLength(5, ErrorMessage = "Слишком уж мощный двигатель, не бывает такого, давай поменьше :)")]
        public string EngineCapacity { get; set; }

        [DisplayName("Тип двигателя")]
        public EngineType? EngineType { get; set; }

        [DisplayName("Коробка передач")]
        public TransmissionType? TransmissionType { get; set; }

        [DisplayName("Привод")]
        public DriveType? DriveType { get; set; }

        [DisplayName("Руль")]
        public SteeringWheelType? SteeringWheelType { get; set; }

        [DisplayName("Тип кузова")]
        public CarcaseType? CarcaseType { get; set; }

        [DisplayName("Количество дверей")]
        public DoorsNumber? DoorsNumber { get; set; }

        [DisplayName("Состояние")]
        public State? State { get; set; }

        [DisplayName("Я хочу")]
        public AdvType Type { get; set; } = AdvType.Sell;

        [DisplayName("Что продаете")]
        public AdvObject? Object { get; set; }

        public bool WithParameters { get; set; } = false;

        public CreateAutoView()
        {
            Years = new List<int>();
            for(int i = DateTime.Now.Year; i > 1959; i--)
            {
                Years.Add(i);
            }
        }
    }
}