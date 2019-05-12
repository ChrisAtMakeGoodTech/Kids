using Kids.EF.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.EF.Services
{
	public static partial class FamilyService
	{

		public async static Task<Models.Family> GetById(int id, Includes includes = null)
		{

			using (var db = new KidsContext())
			{
				var query = db.Family.Where(f => f.Id == id);
				includes?.Add(query);

				return await query.FirstOrDefaultAsync();
			}
		}
		
	}
}
