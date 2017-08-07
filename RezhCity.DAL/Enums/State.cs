using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Enums
{
    public enum State
    {
        [Display(Name = "Не указано")]
        NoState,
        [Display(Name = "Битый")]
        Broken,
        [Display(Name = "Не битый")]
        NotBroken
    }
}
