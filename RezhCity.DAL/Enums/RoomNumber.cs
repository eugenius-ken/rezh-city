using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum RoomNumber
    {
        [Display(Name = "Не указано")]
        NoRoomNumber,
        [Display(Name = "Студия")]
        Studio,
        [Display(Name = "1")]
        One,
        [Display(Name = "2")]
        Two,
        [Display(Name = "3")]
        Three,
        [Display(Name = "4")]
        Four,
        [Display(Name = "5 и более")]
        FiveAndMore
    }
}
