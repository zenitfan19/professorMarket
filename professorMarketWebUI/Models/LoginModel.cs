using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string password { get; set; }
    }

    public class CustomSerializeModel
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

    }
}