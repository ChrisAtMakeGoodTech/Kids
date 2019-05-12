using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.Data.Models
{
	public class PointLogEntry
	{

		internal static PointLogEntry Create(EF.Models.PointLogEntry log)
		{
			return log == null ? null : new PointLogEntry(log);
		}

		internal static IReadOnlyCollection<PointLogEntry> Create(IEnumerable<EF.Models.PointLogEntry> logs)
		{
			return logs?.Select(l => Create(l)).ToList();
		}

		private PointLogEntry(EF.Models.PointLogEntry log)
		{
			Id = log.Id;
			KidId = log.KidId;
			FamilyId = log.FamilyId;
			EventId = log.EventId;
			Timestamp = log.Timestamp;
			Points = log.Points;
			Note = log.Note;
		}

		public int Id { get; }
		public int KidId { get; }
		public int FamilyId { get; }
		public int? EventId { get; }
		public DateTime Timestamp { get; }
		public int Points { get; }
		public string Note { get; }

		//public virtual Event Event { get; set; }
		//public virtual Family Family { get; set; }
		//public virtual Kid Kid { get; set; }
	}
}
