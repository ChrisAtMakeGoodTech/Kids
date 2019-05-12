using Kids.EF.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.EF.Services
{
	public static class FamilyService
	{
		public async static Task<Models.Family> GetById(int id)
		{

			using (var db = new KidsContext())
			{
				var query = db.Family.Where(f => f.Id == id);

				//AddIncludes(ref query, includes);

				return await query.FirstOrDefaultAsync();
			}
		}
	}
}
