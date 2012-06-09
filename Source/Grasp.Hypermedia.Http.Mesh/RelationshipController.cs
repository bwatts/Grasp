using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class RelationshipController : ApiController
	{
		public static readonly ResourceUriPart Root = RelationshipsController.Route.ThenParameter("name");

		public static ResourceUriPart GetUri(Relationship relationship)
		{
			Contract.Requires(relationship != null);

			return RelationshipsController.Route.Then(relationship.Name);
		}

		private readonly IRelationshipRepository _relationshipRepository;

		public RelationshipController(IRelationshipRepository relationshipRepository)
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