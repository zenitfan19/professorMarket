using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace professorMarketWebUI.CustomAuth
{
    public class CustomPrincipal : IPrincipal
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return Roles.Provider.IsUserInRole(Identity.Name, role);
        }
        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}