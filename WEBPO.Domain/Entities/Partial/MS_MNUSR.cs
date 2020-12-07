using System;
using System.Collections.Generic;

namespace WEBPO.Domain.Entities
{
    public partial class MS_MNUSR : BaseEntity
    {
        //navigation properties
        
        public virtual MS_MNFUNC MsMnFunc { get; set; }
    }
}
