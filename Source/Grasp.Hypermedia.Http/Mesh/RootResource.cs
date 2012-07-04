using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootResource : ApiController
	{
		public static readonly UriPart Route = UriPart.Root;

		private readonly IRelationshipRepository _relationshipRepository;
		private readonly IMediaTypeRepository _mediaTypeRepository;

		public RootResource(IRelationshipRepository relationshipRepository, IMediaTypeRepository mediaTypeRepository)
		{
			Contract.Requires(relationshipRepository != null);
			Contract.Requires(mediaTypeRepository != null);

			_relationshipRepository = relationshipRepository;
			_mediaTypeRepository = mediaTypeRepository;
		}

		public RootMedia Get()
		{
			var media = new RootMedia();

			RootMedia.DescriptionField.Set(media, "This is my API");
			RootMedia.MediaTypesField.Set(media, new Many<MediaType>(_mediaTypeRepository.GetAll()));
			RootMedia.RelationshipsField.Set(media, new Many<Relationship>(_relationshipRepository.GetAll()));

			return media;
		}
	}
}