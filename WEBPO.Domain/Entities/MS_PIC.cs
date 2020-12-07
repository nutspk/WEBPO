using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Entities
{
    public partial class MS_PIC
    {
        public int IPicId { get; set; }
        public string IVsCd { get; set; }
        public string IPicName { get; set; }
        public string IMail { get; set; }
        public string IMailFlg { get; set; }
        public string ITelNo { get; set; }
        public string IMobileNo { get; set; }
        public string ISectionCd { get; set; }
        public string ILang { get; set; }
    }
}
