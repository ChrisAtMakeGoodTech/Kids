namespace Kids.Data.Includes
{
	public class KidIncludes
	{
		public static KidIncludes None { get; } = new KidIncludes(false, false);

		public static KidIncludes Log { get; } = new KidIncludes(true, false);
		public static KidIncludes Family { get; } = new KidIncludes(false, true);

		public static KidIncludes All { get; } = new KidIncludes(true, true);

		public KidIncludes(bool log, bool family)
		{
			IncludeLog = log;
			IncludeFamily = family;

			DataInclude = new EF.Services.KidService.Includes(IncludeFamily, IncludeLog);
		}

		internal EF.Services.KidService.Includes DataInclude { get; } 

		internal bool IncludeLog { get; }
		internal bool IncludeFamily { get; }
	}
}
