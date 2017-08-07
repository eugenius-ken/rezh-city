using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum Months
    {
        [Display(Name = "Январь")]
        January,
        [Display(Name = "Февраль")]
        February,
        [Display(Name = "Март")]
        March,
        [Display(Name = "Апрель")]
        April,
        [Display(Name = "Май")]
        May,
        [Display(Name = "Июнь")]
        June,
        [Display(Name = "Июль")]
        Jule,
        [Display(Name = "Август")]
        August,
        [Display(Name = "Сентябрь")]
        September,
        [Display(Name = "Октябрь")]
        October,
        [Display(Name = "Ноябрь")]
        November,
        [Display(Name = "Декабрь")]
        December
    }
}
