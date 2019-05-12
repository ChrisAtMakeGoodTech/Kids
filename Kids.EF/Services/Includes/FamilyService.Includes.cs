using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kids.EF.Services
{
	public static partial class FamilyService
	{
		public class Includes
		{

			public Includes(bool user, bool kid, bool @event, bool log)
			{
				User = user;
				Kid = kid;
				Event = @event;
				Log = log;
			}

			internal void Add(IQueryable<Models.Family> query)
			{
				if (User)
				{
					query = query.Include(f => f.UserFamily).ThenInclude(uf => uf.User);
				}

				if (Kid)
				{
					query = query.Include(f => f.KidFamily).ThenInclude(kf => kf.Kid);
				}

				if (Event)
				{
					query = query.Include(f => f.Event);
				}

				if (Log)
				{
					query = query.Include(f => f.PointLogEntry);
				}
			}

			public bool Kid { get; }
			public bool Log { get; }
			public bool User { get; }
			public bool Event { get; }
		}
	}
	
}
