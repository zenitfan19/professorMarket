using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace professorMarketWebUI.Models
{
    public class TutorProfileModel
    {
        public TutorModel Tutor { get; set; }
    }

    public class TutorModel
    {
        public TutorModel() { }
        public TutorModel(BLL.DTO.TutorDTO dbTutor)
        {

            id = dbTutor.id;
            email = dbTutor.email;
            name = dbTutor.name;
            Skype = dbTutor.Skype;
            info = dbTutor.info;
            education = dbTutor.education;
            qualification = dbTutor.qualification;
            adress = dbTutor.adress;            
            regDate = dbTutor.regDate;
            if (dbTutor.birthDate.HasValue)
                birthDate = dbTutor.birthDate;
            avatarId = dbTutor.avatarId;

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
        [Display(Name = "Дата регистрации")]
        public System.DateTime regDate { get; set; }
        [Display(Name = "Образование")]
        public string education { get; set; }
        [Display(Name = "Опыт")]
        public string qualification { get; set; }
    }
}