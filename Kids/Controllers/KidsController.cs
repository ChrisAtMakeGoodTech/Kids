using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kids.Data;
using System.Linq;
using Kids.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kids.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KidsController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Kid>>> Get()
		{
			using (var db = new KidsContext())
			{
				var families = await db.Family.ToListAsync();
				var kids = await db.Kid.Include(k => k.PointLogEntry).Include(k => k.KidFamily).ToListAsync();
				
				var kidFamilies = await db.KidFamily.ToListAsync();
				return kids;
			}
			
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public string Post([FromBody] string value)
		{
			return "kids";
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
