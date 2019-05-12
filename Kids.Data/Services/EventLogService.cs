using Kids.Data.Includes;
using Kids.Data.Models;

using System.Threading.Tasks;
namespace Kids.Data.Services
{
	public static class EventLogService
	{
		public static async Task<bool> Add(int userId, int familyId, int kidId, int? eventId, int points, string note)
		{
			return await EF.Services.EventLogService.Add(userId, familyId, kidId, eventId, points, note);
		}
	}
}
