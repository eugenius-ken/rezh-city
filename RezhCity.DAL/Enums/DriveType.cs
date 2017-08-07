using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum DriveType
    {
        [Display(Name = "Передний")]
        Forward,
        [Display(Name = "Задний")]
        Back,
        [Display(Name = "Полный")]
        Full
    }
}
