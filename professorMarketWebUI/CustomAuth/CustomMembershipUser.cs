using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace professorMarketWebUI.CustomAuth
{
    public class CustomMembershipUser: MembershipUser
    {
        public long UserId { get; set; }
        public string NickName { get; set; }        

        public CustomMembershipUser(UserDTO user) : base("CustomMembership", user.email, user.id, user.name, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.id;
            NickName = user.email;
        }
    }
}