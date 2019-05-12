using Kids.Data.Models;
using System.Threading.Tasks;

namespace Kids.Data.Services
{
	public static class FamilyService
	{
		public async static Task<Family> GetById(int id)
		{
			//if (includes == null) includes = KidIncludes.None;

			var family = await EF.Services.FamilyService.GetById(id);
			return Family.Create(family);

		}
	}
}
