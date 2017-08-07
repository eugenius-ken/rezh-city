using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum AdvType
    {
        [Display(Name = "Продать")]
        Sell,
        [Display(Name = "Купить")]
        Buy,
        [Display(Name = "Обменять")]
        Transfer,
        [Display(Name = "Отдать даром")]
        Give
    }
}
