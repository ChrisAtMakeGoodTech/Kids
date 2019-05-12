using Kids.EF.Internal;
using Kids.EF.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kids.EF.Services
{
	public static partial class EventLogService
	{
		public static async Task<bool> Add(int userId, int familyId, int kidId, int? eventId, int points, string note)
		{
			using (var db = new KidsContext())
			{
				var entry = PointLogEntry.Create(userId, familyId, kidId, eventId, points, note);
				db.PointLogEntry.Add(entry);
				var rowsInserted = await db.SaveChangesAsync();

				if (rowsInserted == 1)
				{
					var parameters = new List<object>(2)
					{
						new NpgsqlParameter("@PointChange", points),
						new NpgsqlParameter("@KidId", kidId)
					};

					var rowsUpdated = await db.Database.ExecuteSqlCommandAsync("UPDATE kid SET points = points + @PointChange WHERE id = @KidId", parameters);
					return rowsUpdated == 1;

				}
				else
				{
					throw new Exception();
				}

			}
		}
	}
}
