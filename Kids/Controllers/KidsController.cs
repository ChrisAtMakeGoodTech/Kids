using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kids.Data.Models;
using System.Threading.Tasks;
using Kids.Data.Includes;
using System.Linq;
using Kids.Data.Services;

namespace Kids.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KidsController : ControllerBase
	{

		[HttpGet]
		public async Task<IEnumerable<Kid>> Get()
		{
			return await KidService.GetAll(KidIncludes.None);
		}

		[HttpPost]
		public async Task<IEnumerable<Kid>> GetUpdates([FromBody] IEnumerable<(int id, int version)> versions)
		{
			var ids = versions.Select(v => v.id);
			var kidVersions = versions.ToDictionary(v => v.id, v => v.version);
			var kids = (await KidService.GetByIds(ids, KidIncludes.None));
			return kids.Where(k => kidVersions[k.Id] < k.Version);
			
		}

		[HttpGet("{id}")]
		public async Task<Kid> Get(int id)
		{
			return await KidService.GetById(id, KidIncludes.None);
		}

	}
}
