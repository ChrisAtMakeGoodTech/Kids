using Kids.EF.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.EF.Services
{
	public static partial class EventService
	{
		public async static Task<Models.Event> GetById(int id, Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				var query = db.Event.Where(e => e.Id == id);
				includes?.Add(ref query);
				return await query.FirstOrDefaultAsync();
			}
		}

		public async static Task<IEnumerable<Models.Event>> GetByFamily(int id, Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				var query = db.Event.Where(e => e.FamilyId == id);
				includes?.Add(ref query);
				return await query.ToListAsync();
			}
		}

	}
}
