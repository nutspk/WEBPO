using System;
using System.Collections.Generic;

namespace WEBPO.Domain.Entities
{
    public partial class TR_PUB
    {
        public int IPubNo { get; set; }
        public string IVsCd { get; set; }
        public DateTime IRegDate { get; set; }
        public string ISubject { get; set; }
        public string IMessage { get; set; }
        public DateTime? IStartDate { get; set; }
        public DateTime? IEndDate { get; set; }
        public string IUserId { get; set; }
        public string IReadFlg { get; set; }
        public string IAllFlg { get; set; }
    }
}
