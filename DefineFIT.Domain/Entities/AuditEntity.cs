using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineFIT.Domain.Entities
{
    public class AuditEntity : BaseEntity
    {
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public string? UpdatedBy { get; private set; }

        public void SetAudit(string user)
        {
            if (Id == 0)
            {
                CreatedAt = DateTime.Now;
                CreatedBy = user;
            }
            else
            {
                UpdatedAt = DateTime.Now;
                UpdatedBy = user;
            }
        }
    }
}
