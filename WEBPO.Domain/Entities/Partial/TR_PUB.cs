using System;
using System.Collections.Generic;

namespace WEBPO.Domain.Entities
{
    public partial class TR_PUB : BaseEntity
    {
        public virtual MS_VS MsVs { get; set; }
    }
}
