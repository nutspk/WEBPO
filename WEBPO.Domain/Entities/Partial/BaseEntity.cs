using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.Entities
{
    public interface IBaseEntity {
        public DateTime? IEntryDate { get; set; }
        public DateTime? IUpdDate { get; set; }
        public string IUpdUserId { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public DateTime? IEntryDate { get; set; }
        public DateTime? IUpdDate { get; set; }
        public string IUpdUserId { get; set; }
    }
}
