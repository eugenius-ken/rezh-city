using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum WallType
    {
        [Display(Name = "Кирпич")]
        Brick,
        [Display(Name = "Брус")]
        Timber,
        [Display(Name = "Бревно")]
        Beam,
        [Display(Name = "Металл")]
        Metal,
        [Display(Name = "Пеноблоки")]
        Foambloc,
        [Display(Name = "Другой")]
        Others
    }
}
