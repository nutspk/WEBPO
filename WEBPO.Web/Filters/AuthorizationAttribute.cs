using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBPO.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public AuthorizationAttribute(params string[] RoleList) : base()
        {
            Roles = string.Join(",", RoleList);
        }
    }
}
