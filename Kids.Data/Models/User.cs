using System;
using System.Collections.Generic;

namespace Kids.Data.Models
{
    public partial class User
    {
        public User()
        {
            UserFamily = new HashSet<UserFamily>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailUpper { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserFamily> UserFamily { get; set; }
    }
}
