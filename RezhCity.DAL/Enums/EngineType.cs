using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum EngineType
    {
        [Display(Name = "Бензин")]
        Benzin,
        [Display(Name = "Газ")]
        Gas,
        [Display(Name = "Дизель")]
        Diesel
    }
}
