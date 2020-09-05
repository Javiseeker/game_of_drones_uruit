using System;
using System.Collections.Generic;

namespace GameOfDrones.API.Models
{
    public partial class Audit
    {
        public int AUid { get; set; }
        public int AAaUid { get; set; }
        public string ADescription { get; set; }
        public DateTime? ADateCreated { get; set; }

        public virtual AuditAction AAaU { get; set; }
    }
}
