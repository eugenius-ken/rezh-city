using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum Category
    {
        [Display(Name = "Авто")]
        Auto,
        [Display(Name = "Мебель")]
        Furniture,
        [Display(Name = "Недвижимость")]
        Realty,
        [Display(Name = "Одежда")]
        Clothes,
        [Display(Name = "Бытовая электроника")]
        Electronic,
        [Display(Name = "Прочее")]
        Other
    }
}
