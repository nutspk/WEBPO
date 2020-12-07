using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WEBPO.Domain.Entities
{
    public partial class MS_PIC : BaseEntity
    {
        public virtual MS_VS MsVs { get; set; }
    }
}
