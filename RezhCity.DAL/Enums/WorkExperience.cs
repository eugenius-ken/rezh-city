using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum WorkExperience
    {
        [Display(Name = "Без опыта")]
        WithoutExp,
        [Display(Name = "До 1 года")]
        OneYear,
        [Display(Name = "1-3 года")]
        ThreeYears,
        [Display(Name = "Более 3 лет")]
        MoreThreeYears
    }
}
