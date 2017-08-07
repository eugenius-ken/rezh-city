using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum TransmissionType
    {
        [Display(Name = "Механика")]
        Mechanic,
        [Display(Name = "Автомат")]
        Automat,
        [Display(Name = "Робот")]
        Robot,
        [Display(Name = "Вариатор")]
        Variator
    }
}
