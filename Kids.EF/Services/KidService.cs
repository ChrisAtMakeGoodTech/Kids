using Kids.EF.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.EF.Services
{
	public static class KidService
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
		}

		public async static Task<Models.Kid> GetById(int id, Includes includes)
		{

			using (var db = new KidsContext())
			{
				var query = db.Kid.Where(k => k.Id == id);

				AddIncludes(ref query, includes);
				
				return await query.FirstOrDefaultAsync();
			}
		}

		public async static Task<IEnumerable<Models.Kid>> GetByIds(IEnumerable<int> ids, Includes includes)
		{

			using (var db = new KidsContext())
			{
				var query = db.Kid.Where(k => ids.Contains(k.Id));

				AddIncludes(ref query, includes);

				return await query.ToListAsync();
			}
		}

		public async static Task<IReadOnlyCollection<Models.Kid>> GetByFamily(int familyId, Includes includes)
		{

			using (var db = new KidsContext())
			{
				var query = db.KidFamily.Where(kf => kf.FamilyId == familyId).Include(kf => kf.Kid).ThenInclude(k => k.PointLogEntry).Select(kf => kf.Kid);

				AddIncludes(ref query, includes);

				return await query.ToListAsync();

			}
		}

		public async static Task<IReadOnlyCollection<Models.Kid>> GetAll(Includes includes)
		{

			using (var db = new KidsContext())
			{
				IQueryable<Models.Kid> query = db.Kid;

				AddIncludes(ref query, includes);

				return await query.ToListAsync();

			}
		}

		private static void AddIncludes(ref IQueryable<Models.Kid> query, Includes includes)
		{
			if (includes == null) return;

			if (includes.Log)
			{
				query = query.Include(k => k.PointLogEntry);
			}
			if (includes.Family)
			{
				query = query.Include(k => k.KidFamily);
			}
		}
	}
}
