using System;
using System.Collections.Generic;
using System.Linq;

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

		//private ICollection<Family> _Family;
		//public ICollection<Family> Family
		//{
		//	get
		//	{
		//		if (KidFamily == null) return null;

		//		if (_Family == null)
		//		{
		//			_Family = KidFamily.Select(kf => kf.Family).ToList();
		//		}

		//		return _Family;
		//	}
		//}
	}
}
