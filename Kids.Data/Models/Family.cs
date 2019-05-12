using System.Collections.Generic;

namespace Kids.Data.Models
{
	public partial class Family
	{
		public Family()
		{
			Event = new HashSet<Event>();
			KidFamily = new HashSet<KidFamily>();
			PointLogEntry = new HashSet<PointLogEntry>();
			UserFamily = new HashSet<UserFamily>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Event> Event { get; set; }
		public virtual ICollection<KidFamily> KidFamily { get; set; }
		public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
		public virtual ICollection<UserFamily> UserFamily { get; set; }
	}
}
