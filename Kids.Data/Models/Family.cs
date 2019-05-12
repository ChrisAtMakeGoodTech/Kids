using System.Collections.Generic;
using System.Linq;

namespace Kids.Data.Models
{
	public class Family
	{
		internal static Family Create(EF.Models.Family family)
		{
			return family == null ? null : new Family(family);
		}

		internal static IReadOnlyCollection<Family> Create(IEnumerable<EF.Models.Family> family)
		{
			return family?.Select(f => Create(f)).ToList();
		}

		private Family(EF.Models.Family family)
		{
			Id = family.Id;
			Name = family.Name;

			Events = new List<object>();
			
			//Event = new HashSet<Event>();
			//KidFamily = new HashSet<KidFamily>();
			//PointLogEntry = new HashSet<PointLogEntry>();
			//UserFamily = new HashSet<UserFamily>();
		}

		public int Id { get; }
		public string Name { get; }
		public IEnumerable<object> Events { get; }

		//public virtual ICollection<Event> Event { get; set; }
		//public virtual ICollection<KidFamily> KidFamily { get; set; }
		//public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
		//public virtual ICollection<UserFamily> UserFamily { get; set; }
	}
}
