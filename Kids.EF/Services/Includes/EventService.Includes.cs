using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kids.EF.Services
{
	public static partial class EventService
	{
		public class Includes
		{

			public Includes(bool family)
			{
				Family = family;
			}

			internal void Add(ref IQueryable<Models.Event> query)
			{
				if (Family)
				{
					query = query.Include(e => e.Family);
				}
			}

			public bool Family { get; }
		}
	}
}
