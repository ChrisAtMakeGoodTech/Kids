﻿using System;
using System.Collections.Generic;

namespace Kids.Data
{
    public partial class UserFamily
    {
        public int UserId { get; set; }
        public int FamilyId { get; set; }

        public virtual Family Family { get; set; }
        public virtual User User { get; set; }
    }
}
