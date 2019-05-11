using Microsoft.AspNetCore.Builder;

namespace Kids.Internal
{

	internal class KidsStaticFileOptions : StaticFileOptions
	{
		public KidsStaticFileOptions() : base()
		{
			ContentTypeProvider = new IDontKnowHowContentProvidersWork(ContentTypeProvider);
		}
	}

}
