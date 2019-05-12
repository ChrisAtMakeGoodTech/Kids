using Kids.Data.Includes;
using Kids.Data.Models;
using System.Threading.Tasks;

namespace Kids.Data.Services
{
	public static class FamilyService
	{
		public async static Task<Family> GetById(int id, FamilyIncludes includes = null)
			=> Family.Create(await EF.Services.FamilyService.GetById(id, includes?.DataInclude));
	}
}
