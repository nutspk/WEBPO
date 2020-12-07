using System;
using System.Collections.Generic;

namespace WEBPO.Domain.Entities
{
    public partial class MS_MNCAT : BaseEntity
    {
        //navigation properties
        public virtual ICollection<MS_MNFUNC> MsMuFuncs { get; set; } = new HashSet<MS_MNFUNC>();
    }
}
