using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Core.Interfaces
{
    public interface IUserResolverService
    {

        bool IsAuthenticated { get; }
        bool IsAdmin { get;}
        string RoleText { get; }
        string UserName { get; }
        string UserID { get; }
        string Email { get;  }
        string CompanyName { get; }
        string VendorCode { get; }
    }
}
