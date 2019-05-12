using System.Collections.Generic;
using System.Linq;

namespace Kids.Data.Models
{
	public class Event
	{
		internal static Event Create(EF.Models.Event @event)
			=> new Event(@event);

		internal static IEnumerable<Event> Create(IEnumerable<EF.Models.Event> events)
			=> events.Select(e => Create(e));

		private Event(EF.Models.Event @event)
		{

			Id = @event.Id;
			FamilyId = @event.FamilyId;
			Description = @event.Description;
			Points = @event.Points;

			//PointLogEntry = new HashSet<PointLogEntry>();
		}

		public int Id { get; }
		public int FamilyId { get; }
		public string Description { get; }
		public int Points { get; }

		//public virtual Family Family { get; set; }
		//public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
	}
}
