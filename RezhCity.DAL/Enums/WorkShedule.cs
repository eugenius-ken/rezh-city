using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum WorkShedule
    {
        [Display(Name = "Любой")]
        Any,
        [Display(Name = "Полный день")]
        Full,
        [Display(Name = "Удаленная работа")]
        Remote,
        [Display(Name = "Гибкий график")]
        Flexible,
        [Display(Name = "Сменный график")]
        Shift,
        [Display(Name = "Вахта")]
        Tour
    }
}
