using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Http.Mesh
{
	public sealed class RelationshipsController : ApiController
	{
		public static readonly ResourceUriPart Route = RootController.Route.Then(Relationship.Meta).Then(Relationship.Plural);

		private readonly IRelationshipRepository _relationshipRepository;

		public RelationshipsController(IRelationshipRepository relationshipRepository)
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