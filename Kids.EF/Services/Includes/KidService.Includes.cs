using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kids.EF.Services
{
	public static partial class KidService
	{
		public class Includes
		{
			public Includes(bool family, bool log)
			{
				Family = family;
				Log = log;
			}

			public bool Family { get; }
			public bool Log { get; }

			public void Add(IQueryable<Models.Kid> query)
			{
				if (Log)
				{
					query = query.Include(k => k.PointLogEntry);
				}
				if (Family)
				{
					query = query.Include(k => k.KidFamily).ThenInclude(kf => kf.Family);
				}
			}
		}

	}
}
