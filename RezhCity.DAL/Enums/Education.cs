using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum Education
    {
        [Display(Name = "Нет")]
        No,
        [Display(Name = "Среднее")]
        Secondary,
        [Display(Name = "Средне-специальное")]
        Vocational,
        [Display(Name = "Неполное высшее")]
        IncompleteHigher,
        [Display(Name = "Высшее")]
        Higher
    }
}
