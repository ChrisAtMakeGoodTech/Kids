namespace Kids.Objects
{
	public class AddEventRequest
	{
		public AddEventRequest(int userId, int familyId, int kidId, int points, string note, int? eventId)
		{
			UserId = userId;
			FamilyId = familyId;
			KidId = kidId;
			Points = points;
			Note = note;
			EventId = eventId;
		}

		public int UserId { get; }
		public int FamilyId { get; }
		public int KidId { get; }
		public int Points { get; }

		public string Note { get; }
		public int? EventId { get; }
		
	}
}
