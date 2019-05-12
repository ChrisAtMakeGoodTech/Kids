using System;

namespace Kids.EF.Models
{
	public partial class PointLogEntry
	{
		public static PointLogEntry Create(int userId, int familyId, int kidId, int? eventId, int points, string note)
		{
			return new PointLogEntry(userId, familyId, kidId, eventId, points, note);
		}

		public PointLogEntry() { }

		private PointLogEntry(int userId, int familyId, int kidId, int? eventId, int points, string note) : this()
		{
			KidId = kidId;
			FamilyId = familyId;
			UserId = userId;
			EventId = eventId;
			Points = points;
			Note = note;
		}

		public int Id { get; set; }
		public int KidId { get; set; }
		public int FamilyId { get; set; }
		public int UserId { get; set; }
		public int? EventId { get; set; }
		public DateTime Timestamp { get; set; }
		public int Points { get; set; }
		public string Note { get; set; }

		public virtual Event Event { get; set; }
		public virtual Family Family { get; set; }
		public virtual Kid Kid { get; set; }
		public virtual User User { get; set; }
	}
}
