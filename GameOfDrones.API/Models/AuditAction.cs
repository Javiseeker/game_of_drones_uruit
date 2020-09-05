using System;
using System.Collections.Generic;

namespace GameOfDrones.API.Models
{
    public partial class AuditAction
    {
        public AuditAction()
        {
            Audit = new HashSet<Audit>();
        }

        public int AaUid { get; set; }
        public string AaName { get; set; }

        public virtual ICollection<Audit> Audit { get; set; }
    }
}
