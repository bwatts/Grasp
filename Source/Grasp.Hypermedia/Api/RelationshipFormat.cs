using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia.Api
{
	public sealed class RelationshipFormat : MediaFormat<Relationship>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.hypermedia.relationship");

		public RelationshipFormat() : base(MediaType)
		{}

		protected override Relationship ConvertFromRepresentation(MRepresentation representation)
		{
			throw new NotImplementedException();
		}

		protected override MRepresentation ConvertToRepresentation(Relationship media)
		{
			throw new NotImplementedException();
		}
	}
}