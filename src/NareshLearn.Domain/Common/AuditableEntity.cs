using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; protected set; }

        public void MarkUpdated()
        {
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
