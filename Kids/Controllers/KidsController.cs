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
			return await KidService.GetAll(KidIncludes.Log);
		}

		[HttpPost]
		public async Task<IEnumerable<Kid>> GetUpdates([FromBody] IEnumerable<Objects.IdVersion> request)
		{
			var ids = request.Select(v => v.Id);
			var kidVersions = request.ToDictionary(v => v.Id, v => v.Version);
			var kids = await KidService.GetByIds(ids, KidIncludes.None);
			return kids.Where(k => kidVersions[k.Id] < k.Version).ToArray();

		}

		[HttpGet("{id}")]
		public async Task<Kid> Get(int id)
		{
			return await KidService.GetById(id, KidIncludes.None);
		}

	}
}
