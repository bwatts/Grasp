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
	public sealed class MediaTypesController : ApiController
	{
		public static readonly ResourceUriPart Route = RootController.Route.Then(Relationship.Meta).Then(MediaType.PluralRelationship);

		private readonly IMediaTypeRepository _mediaTypeRepository;

		public MediaTypesController(IMediaTypeRepository mediaTypeRepository)
		{
			Contract.Requires(mediaTypeRepository != null);

			_mediaTypeRepository = mediaTypeRepository;
		}

		public IEnumerable<MediaType> Get()
		{
			return _mediaTypeRepository.GetAll();
		}
	}
}