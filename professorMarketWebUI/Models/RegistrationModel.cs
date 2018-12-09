using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class RegistrationModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Неверный формат")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Введенные пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        public string retryPassword { get; set; }
        [Required]
        [Display(Name = "Статус")]
        public string role { get; set; }
        [Required]
        [Display(Name = "ФИО")]
        public string name { get; set; }        
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public System.DateTime birthDate { get; set; }
    }
}