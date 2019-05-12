using System.Collections.Generic;
using System.Threading.Tasks;
using Kids.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kids.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FamilyController : ControllerBase
	{
		// GET api/values
		[HttpGet("{id}")]
		public async Task<object> Index(int id)
		{
			return await FamilyService.GetById(id);
		}

		[HttpPost()]
		public ActionResult<IEnumerable<string>> Actions([FromBody] int id)
		{
			return new string[] { "value1", "value2" };
		}

	}
}
