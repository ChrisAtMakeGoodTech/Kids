﻿using Kids.Data.Includes;
using Kids.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.Data.Models
{
	public class Kid
	{

		internal static Kid Create(EF.Models.Kid kid)
		{
			return kid == null ? null : new Kid(kid);
		}

		internal static IReadOnlyCollection<Kid> Create(IEnumerable<EF.Models.Kid> kids)
		{
			return kids?.Select(k => Create(k)).ToList();
		}

		private Kid(EF.Models.Kid kid)
		{
			Name = kid.Name;
			Id = kid.Id;
			Points = kid.Points;
			BirthDate = kid.BirthDate;
			Version = kid.Version;

			Log = kid.PointLogEntry?.Select(e => PointLogEntry.Create(e)).ToList();

		}

		public int Id { get; }
		public string Name { get; }
		public int Points { get; }
		public DateTime? BirthDate { get; }
		public int Version { get; }

		public IReadOnlyCollection<PointLogEntry> Log { get; }

		//public virtual ICollection<Family> Family { get; set; }
		//public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
	}
}
