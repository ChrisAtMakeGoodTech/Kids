using Kids.EF.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.EF.Services
{
	public static partial class KidService
	{

		public async static Task<Models.Kid> GetById(int id, Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				var query = db.Kid.Where(k => k.Id == id);
				includes?.Add(ref query);
				return await query.FirstOrDefaultAsync();
			}
		}

		public async static Task<IEnumerable<Models.Kid>> GetByIds(IEnumerable<int> ids, Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				var query = db.Kid.Where(k => ids.Contains(k.Id));
				includes?.Add(ref query);
				return await query.ToListAsync();
			}
		}

		public async static Task<IReadOnlyCollection<Models.Kid>> GetByFamily(int familyId, Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				var query = db.KidFamily.Where(kf => kf.FamilyId == familyId).Include(kf => kf.Kid).ThenInclude(k => k.PointLogEntry).Select(kf => kf.Kid);
				includes?.Add(ref query);
				return await query.ToListAsync();
			}
		}

		public async static Task<IReadOnlyCollection<Models.Kid>> GetAll(Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				IQueryable<Models.Kid> query = db.Kid;
				includes?.Add(ref query);
				return await query.ToListAsync();
			}
		}

	}
}
