using System;
using System.Collections.Generic;

namespace WEBPO.Domain.Entities
{
    public partial class MS_USER
    {
        public string IUserId { get; set; }
        public string IUserName { get; set; }
        public string IVsCd { get; set; }
        public string IMail { get; set; }
        public string IMailFlg { get; set; }
        //public string ITelNo { get; set; }
        public string ISectionCd { get; set; }
        public string ILang { get; set; }
        public string IUserType { get; set; }
        //public string IGroupId { get; set; }
        //public string IMngType { get; set; }
    }
}
