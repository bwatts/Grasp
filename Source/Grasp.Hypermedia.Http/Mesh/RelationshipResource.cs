using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class RelationshipResource : HttpResource
	{
		public static readonly UriPart Root = RelationshipsResource.Route.ThenParameter("name");

		public static UriPart GetUri(Relationship relationship)
		{
			Contract.Requires(relationship != null);

			return RelationshipsResource.Route.Then(relationship.Name);
		}

		private readonly IRelationshipRepository _relationshipRepository;

		public RelationshipResource(IRelationshipRepository relationshipRepository)
		{
			Contract.Requires(relationshipRepository != null);

			_relationshipRepository = relationshipRepository;
		}

		public Relationship Get(string name)
		{
			return _relationshipRepository.GetByName(name);
		}
	}
}