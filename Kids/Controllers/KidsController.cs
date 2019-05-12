using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kids.Data.Models;
using System.Threading.Tasks;
using Kids.Data.Includes;
using System.Linq;
using Kids.Data.Services;
using Microsoft.AspNetCore.Http;
using Kids.Objects;

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
		public async Task<IEnumerable<Kid>> GetUpdates([FromBody] IEnumerable<IdVersion> request)
		{
			var kidVersions = request.ToDictionary(v => v.Id, v => v.Version);
			var ids = kidVersions.Keys;
			var kids = await KidService.GetByIds(ids, KidIncludes.Log);
			return kids.Where(k => kidVersions[k.Id] < k.Version).ToArray();

		}

		[HttpGet("{id}")]
		public async Task<Kid> Get(int id)
		{
			return await KidService.GetById(id, KidIncludes.None);
		}

		[Route("updatePoints")]
		[HttpPost]
		public async Task<IActionResult> UpdatePoints()
		{
			return StatusCode(StatusCodes.Status404NotFound);
			//return StatusCode(StatusCodes.Status200OK);
		}

		[Route("addEvent")]
		[HttpPost]
		public async Task<IActionResult> AddEvent([FromBody]AddEventRequest request)
		{
			var success = await EventLogService.Add(request.UserId, request.FamilyId, request.KidId, request.EventId, request.Points, request.Note);
			if (success)
			{
				return StatusCode(StatusCodes.Status201Created);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
