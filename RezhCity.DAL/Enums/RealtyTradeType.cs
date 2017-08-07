using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum RealtyTradeType
    {
        [Display(Name = "Продать")]
        Sell,
        [Display(Name = "Купить")]
        Buy,
        [Display(Name = "Сдать")]
        RentOff,
        [Display(Name = "Снять")]
        RentOn,
        [Display(Name = "Обменять")]
        Exchange
    }
}
