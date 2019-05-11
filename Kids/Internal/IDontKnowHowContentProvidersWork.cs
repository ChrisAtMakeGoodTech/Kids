using Microsoft.AspNetCore.StaticFiles;

namespace Kids.Internal
{

	internal class IDontKnowHowContentProvidersWork : IContentTypeProvider
	{
		public IDontKnowHowContentProvidersWork(IContentTypeProvider realProvider) : base()
		{
			RealProvider = realProvider;
		}

		private IContentTypeProvider RealProvider;

		public bool TryGetContentType(string subpath, out string contentType)
		{
			if (subpath.ToLower().EndsWith(".mjs"))
			{
				contentType = "application/javascript";
				return true;
			}
			return RealProvider.TryGetContentType(subpath, out contentType);
		}
	}

}
