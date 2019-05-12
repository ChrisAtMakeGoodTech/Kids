using Kids.Data.Includes;
using Kids.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kids.Data.Services
{
	public static class KidService
	{
		public async static Task<Kid> GetById(int id, KidIncludes includes = null)
			=> Kid.Create(await EF.Services.KidService.GetById(id, includes?.DataInclude));

		public async static Task<IEnumerable<Kid>> GetByIds(IEnumerable<int> ids, KidIncludes includes = null)
			=> Kid.Create(await EF.Services.KidService.GetByIds(ids, includes?.DataInclude));

		public async static Task<IReadOnlyCollection<Kid>> GetByFamily(int id, KidIncludes includes = null)
			=> Kid.Create(await EF.Services.KidService.GetByFamily(id, includes?.DataInclude));

		public async static Task<IReadOnlyCollection<Kid>> GetAll(KidIncludes includes = null)
			=> Kid.Create(await EF.Services.KidService.GetAll(includes?.DataInclude));
	}
}
