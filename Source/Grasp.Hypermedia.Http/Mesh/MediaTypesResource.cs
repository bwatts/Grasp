using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class MediaTypesResource : HttpResource
	{
		public static readonly UriPart Route = RootResource.Route.Then(Relationship.Meta).Then(MediaType.PluralRelationship);

		private readonly IMediaTypeRepository _mediaTypeRepository;

		public MediaTypesResource(IMediaTypeRepository mediaTypeRepository)
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