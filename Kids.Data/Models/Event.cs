using System;
using System.Collections.Generic;

namespace Kids.Data.Models
{
    public partial class Event
    {
        public Event()
        {
            PointLogEntry = new HashSet<PointLogEntry>();
        }

        public int Id { get; set; }
        public int FamilyId { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }

        public virtual Family Family { get; set; }
        public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
    }
}
