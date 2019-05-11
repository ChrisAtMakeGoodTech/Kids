using System;
using System.Collections.Generic;

namespace Kids.Data.Models
{
    public partial class Kid
    {
        public Kid()
        {
            KidFamily = new HashSet<KidFamily>();
            PointLogEntry = new HashSet<PointLogEntry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Version { get; set; }

        public virtual ICollection<KidFamily> KidFamily { get; set; }
        public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
    }
}
