using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class RequestForAdminModel
    {
        public long userId { get; set; }
        public long? adminId { get; set; }
        [Display(Name = "Вид обращения")]
        public long typeId { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string text { get; set; }
        public string status { get; set; }
        public System.DateTime date { get; set; }
    }
}