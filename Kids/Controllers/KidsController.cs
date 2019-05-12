using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kids.Data.Models;
using System.Threading.Tasks;
using Kids.Data.Includes;

namespace Kids.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KidsController : ControllerBase
	{

		[HttpGet]
		public async Task<IEnumerable<Kid>> Get()
		{
			return new List<Kid>
			{
				await Kid.GetById(3, KidIncludes.None)
			};
		}

		[HttpGet("{id}")]
		public async Task<Kid> Get(int id)
		{
			return await Kid.GetById(id, KidIncludes.None);
		}

	}
}
