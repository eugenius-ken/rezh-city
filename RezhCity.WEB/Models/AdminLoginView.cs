using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RezhCity.WEB.Models
{
    public class AdminLoginView
    {
        [Required]
        [StringLength(50)]
        [DisplayName("Логин")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}