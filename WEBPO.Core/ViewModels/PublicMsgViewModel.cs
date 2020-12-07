using System;
using System.Collections.Generic;
using System.Text;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.ViewModels
{
    public class PublicMsgViewModel
    {
        public int PublicNo { get; set; }
        public DateTime PublicDate { get; set; }
        public int ItemSeq { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MS_VS MsVs { get; set; }
    }
}
