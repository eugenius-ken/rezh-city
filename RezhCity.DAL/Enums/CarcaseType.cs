using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum CarcaseType
    {
        [Display(Name = "Седан")]
        Sedan,
        [Display(Name = "Хетчбэк")]
        HatchBack,
        [Display(Name = "Универсал")]
        Universal,
        [Display(Name = "Внедорожник")]
        SUV,
        [Display(Name = "Кроссовер")]
        Crossover,
        [Display(Name = "Минивэн")]
        Minivan,
        [Display(Name = "Фургон")]
        Van,
        [Display(Name = "Микроавтобус")]
        Minibus
    }
}
