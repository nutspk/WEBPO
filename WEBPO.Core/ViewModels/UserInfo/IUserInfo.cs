using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBPO.Core.ViewModels.UserInfo
{
    public interface IUserInfo
    {
        string UserId { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string CompanyName { get; set; }
        string VendorCode { get; set; }
        string RoleText { get; set; }
        bool IsAdmin { get; set; }
    }
}
