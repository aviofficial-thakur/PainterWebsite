using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PainterWebsite.Filter
{
    public class CustomPrincipal : IPrincipal, IIdentity
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            return false; 
        }

        public string AuthenticationType => "CustomAuthentication";
        public bool IsAuthenticated => true;
        public string Name { get; private set; }

        public CustomPrincipal(string userId)
        {
            Identity = this;
            Name = userId;
        }
    }
}