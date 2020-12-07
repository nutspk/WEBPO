
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Entities
{
    public partial class MS_VS : BaseEntity
    {
        public string VendorTypeText => (IVsType == "01") ? "EDI" : "NON EDI";
        public string VendorDescText => IVsCd + " : " + IVsDesc + " ("+ VendorTypeText + ")";
        public virtual ICollection<MS_USER> MsUsers { get; set; } = new HashSet<MS_USER>();
        public virtual ICollection<TR_PUB> TrPubs { get; set; } = new HashSet<TR_PUB>();
        public virtual ICollection<MS_PIC> MsContrPerson { get; set; } = new HashSet<MS_PIC>();
    }
}
