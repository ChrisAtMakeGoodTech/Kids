using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kids.Objects
{
	public class IdVersion
	{
		public IdVersion(int id, int version)
		{
			Id = id;
			Version = version;
		}

		public int Id { get; }
		public int Version { get; }
	}
}
