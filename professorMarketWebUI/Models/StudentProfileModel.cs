using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class StudentProfileModel
    {
        public StudentModel Student { get; set; }
    }

        public class StudentModel
        {
        public StudentModel() { }
        public StudentModel(BLL.DTO.StudentDTO dbStudent)
        {

            id = dbStudent.id;
            email = dbStudent.email;
            name = dbStudent.name;
            Skype = dbStudent.Skype;
            info = dbStudent.info;
            adress = dbStudent.adress;
            lesonsLevel = dbStudent.lessonsLevelId;
            regDate = dbStudent.regDate;
            if(dbStudent.birthDate.HasValue)
            birthDate = dbStudent.birthDate;
            avatarId = dbStudent.avatarId;

        }
            public long id { get; set; }
            public long? avatarId { get; set; }
            [Required]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Неверный формат")]
            public string email { get; set; }
            [Required]
            [Display(Name = "ФИО")]
            public string name { get; set; }            
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Display(Name = "Дата рождения")]
            public Nullable<System.DateTime> birthDate { get; set; }
            [Display(Name = "Skype")]
            public string Skype { get; set; }
            [Display(Name = "О себе")]
            public string info { get; set; }
            [Display(Name = "Адрес")]
            public string adress { get; set; }
            [Display(Name = "Уровень обучения")]
            public long? lesonsLevel { get; set; }
            [Display(Name = "Дата регистрации")]
            public System.DateTime regDate { get; set; }           

    }

    
}