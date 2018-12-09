using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class StudentDTO
    {
        public StudentDTO() { }

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
        public Nullable<long> lessonsLevelId { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public System.DateTime regDate { get; set; }
    }
}
