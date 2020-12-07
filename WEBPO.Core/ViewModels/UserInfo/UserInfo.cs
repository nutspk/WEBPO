
using WEBPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBPO.Core.ViewModels.UserInfo
{
    public class UserInfo : IUserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string VendorCode { get; set; }
        public string RoleText { get; set; }
        public bool IsAdmin { get; set; }
        public UserInfo(MS_USER user)
        {
            UserId = user.IUserId;
            UserName = user.IUserName;
            Email = user.IMail;
            CompanyName = user.IUserType;
            VendorCode = user.IVsCd;
            RoleText = user.RoleText;
            IsAdmin = (user.IUserType == "99");
        }

        public UserInfo() { }
    }
}
