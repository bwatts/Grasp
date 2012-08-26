using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Api
{
	public class RootResource : ApiController
	{
		public static readonly UriPath Route = UriPath.Root;

		private readonly IRelationshipRepository _relationshipRepository;
		private readonly IMediaTypeRepository _mediaTypeRepository;

		public RootResource(IRelationshipRepository relationshipRepository, IMediaTypeRepository mediaTypeRepository)
		{
			Contract.Requires(relationshipRepository != null);
			Contract.Requires(mediaTypeRepository != null);

			_relationshipRepository = relationshipRepository;
			_mediaTypeRepository = mediaTypeRepository;
		}

		public HttpApi Get()
		{
			var media = new HttpApi();

			HypermediaApi.RelationshipsField.Set(media, new Many<Relationship>(_relationshipRepository.GetAll()));
			HypermediaApi.NameField.Set(media, "TODO: Name API");
			HttpApi.MediaTypesField.Set(media, new Many<MediaType>(_mediaTypeRepository.GetAll()));

			return media;
		}
	}
}