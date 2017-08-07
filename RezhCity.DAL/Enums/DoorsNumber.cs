using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum DoorsNumber
    {
        [Display(Name = "2")]
        Two,
        [Display(Name = "3")]
        Three,
        [Display(Name = "4")]
        Four,
        [Display(Name = "5")]
        Five
    }
}
