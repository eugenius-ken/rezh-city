using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum AdvObject
    {
        [Display(Name = "Автомобиль")]
        Auto,
        [Display(Name = "Мотоцикл")]
        Moto,
        [Display(Name = "Запчасти")]
        Spares,
        [Display(Name = "Квартира")]
        Appartments,
        [Display(Name = "Комната")]
        Room,
        [Display(Name = "Дом")]
        House,
        [Display(Name = "Гараж")]
        Garage,
        [Display(Name = "Другое")]
        Other
    }
}
