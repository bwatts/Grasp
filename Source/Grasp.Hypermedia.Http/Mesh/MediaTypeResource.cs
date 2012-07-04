using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class MediaTypeResource : HttpResource
	{
		public static readonly UriPart Root = MediaTypesResource.Route.ThenParameter("name");

		public static UriPart GetUri(MediaType mediaType)
		{
			Contract.Requires(mediaType != null);

			return MediaTypesResource.Route.Then(mediaType.Name);
		}

		private readonly IMediaTypeRepository _mediaTypeRepository;

		public MediaTypeResource(IMediaTypeRepository mediaTypeRepository)
		{
			Contract.Requires(mediaTypeRepository != null);

			_mediaTypeRepository = mediaTypeRepository;
		}

		public MediaType Get(string name)
		{
			return _mediaTypeRepository.GetByName(name);
		}
	}
}