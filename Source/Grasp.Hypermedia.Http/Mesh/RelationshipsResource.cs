using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class RelationshipsResource : HttpResource
	{
		public static readonly UriPart Route = RootResource.Route.Then(Relationship.Meta).Then(Relationship.Plural);

		private readonly IRelationshipRepository _relationshipRepository;

		public RelationshipsResource(IRelationshipRepository relationshipRepository)
		{
			Contract.Requires(relationshipRepository != null);

			_relationshipRepository = relationshipRepository;
		}

		public IEnumerable<Relationship> Get()
		{
			return _relationshipRepository.GetAll();
		}
	}
}