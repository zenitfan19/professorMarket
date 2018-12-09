using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class SubjectModel
    {
        [Required]
        [Display(Name = "Название")]
        public string name { get; set; }
        [Display(Name = "Группа предметов")]
        public long subjectTypeId { get; set; }
    }
}