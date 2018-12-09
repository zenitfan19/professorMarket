using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class UserDTO
    {
        public UserDTO() { }

        public long id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }
        public bool emailVerified { get; set; }        
        public Nullable<System.DateTime> birthDate { get; set; }
        public System.DateTime regDate { get; set; }
        public string role { get; set; }
    }
}
