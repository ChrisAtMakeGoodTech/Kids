using System.Collections.Generic;

namespace Kids.EF.Models
{
	public partial class User
	{
		public User()
		{
			UserFamily = new HashSet<UserFamily>();
			PointLogEntry = new HashSet<PointLogEntry>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string EmailUpper { get; set; }
		public string Password { get; set; }

		public virtual ICollection<UserFamily> UserFamily { get; set; }
		public virtual ICollection<PointLogEntry> PointLogEntry { get; set; }
	}
}
