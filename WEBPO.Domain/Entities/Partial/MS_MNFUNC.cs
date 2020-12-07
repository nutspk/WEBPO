using System;
using System.Collections.Generic;

namespace WEBPO.Domain.Entities
{
    public partial class MS_MNFUNC : BaseEntity
    {
        //navigation properties
        public virtual MS_MNCAT MsMnCat { get; set; }
        public virtual ICollection<MS_MNUSR> MsMnUsrs { get; set; } = new HashSet<MS_MNUSR>();
    }
}
