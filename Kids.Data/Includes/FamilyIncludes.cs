namespace Kids.Data.Includes
{
	public class FamilyIncludes
	{
		public static FamilyIncludes None { get; } = new FamilyIncludes(false, false, false, false);

		public static FamilyIncludes User { get; } = new FamilyIncludes(true, false, false, false);
		public static FamilyIncludes Kid { get; } = new FamilyIncludes(false, true, false, false);
		public static FamilyIncludes Event { get; } = new FamilyIncludes(false, false, true, false);
		public static FamilyIncludes Log { get; } = new FamilyIncludes(false, false, false, true);

		public static FamilyIncludes All { get; } = new FamilyIncludes(true, true, true, true);

		
		public FamilyIncludes(bool user, bool kid, bool @event, bool log)
		{
			IncludeUser = user;
			IncludeKid = kid;
			IncludeEvent = @event;
			IncludeLog = log;

			DataInclude = new EF.Services.FamilyService.Includes(user, kid, @event, log);
		}

		internal EF.Services.FamilyService.Includes DataInclude { get; }

		internal bool IncludeUser { get; }
		internal bool IncludeKid { get; }
		internal bool IncludeEvent { get; }
		internal bool IncludeLog { get; }		
		
	}
}
