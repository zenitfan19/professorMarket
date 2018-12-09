using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class PasswordChangeModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string oldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Введенные пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        public string retryPassword { get; set; }
    }
}