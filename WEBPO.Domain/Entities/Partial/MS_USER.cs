using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBPO.Domain.Entities
{
    public partial class MS_USER : BaseEntity
    {
        public string IUserPwd { get; set; }
        public string IResetPin { get; set; }

        //[ForeignKey(nameof(IVsCd))]
        public virtual MS_VS MS_VS { get; set; }
        
        public bool IsAdmin => (IUserType == "99");
        public string RoleText => IsAdmin ? "ADMIN" : "USER";
    }
}
