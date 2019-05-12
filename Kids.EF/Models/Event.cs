using System.Collections.Generic;

namespace Kids.EF.Models
{
	public partial class Event
	{
		public static Event Create(int familyId, string description, int points)
		{
			return new Event(familyId, description, points);
		}

		public static Event Create(Family family, string description, int points)
		{
			return new Event(family.Id, description, points);
		}

		public Event()
		{
			PointLogEntry = new HashSet<PointLogEntry>();
		}

		private Event(int familyId, string description, int points) : this()
		{
			FamilyId = familyId;
			Description = description;
			Points = points;
		}

		public int Id { get; set; }
		public int FamilyId { get; set; }
		public string Description { get; set; }
		public int Points { get; set; }

		public virtual Family Family { get; set; }
		public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
	}
}
