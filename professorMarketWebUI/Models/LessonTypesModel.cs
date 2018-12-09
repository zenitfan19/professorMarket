using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class LessonTypesModel
    {
        [Required]
        [Display(Name = "Название")]
        public string name { get; set; }
    }
}