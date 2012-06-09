using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Grasp.Hypermedia.Http.Media;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class MediaTypeController : ApiController
	{
		public static readonly ResourceUriPart Root = MediaTypesController.Route.ThenParameter("name");

		public static ResourceUriPart GetUri(MediaType mediaType)
		{
			Contract.Requires(mediaType != null);

			return MediaTypesController.Route.Then(mediaType.Name);
		}

		private readonly IMediaTypeRepository _mediaTypeRepository;

		public MediaTypeController(IMediaTypeRepository mediaTypeRepository)
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