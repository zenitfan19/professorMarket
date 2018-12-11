using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class TutorDTO
    {
        public TutorDTO() { }

        public long id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }
        public bool emailVerified { get; set; }
        public Nullable<long> avatarId { get; set; }
        public string Skype { get; set; }
        public string info { get; set; }
        public string adress { get; set; }
        public string education { get; set; }
        public string qualification { get; set; }
        public Nullable<bool> isApproved { get; set; }
        public Nullable<double> rating { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public System.DateTime regDate { get; set; }

        public virtual List<TutorLessonLevelDTO> Levels { get; set; }
        public virtual List<TutorLessonTypeDTO> Types { get; set; }
        public virtual List<TutorSubjectsDTO> Subjects { get; set; }
    }
}
