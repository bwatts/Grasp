using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Relationships
{
	public class TwoWayRelationshipModel : Notion
	{
		public static Field<ReferenceModel> Reference1Field = Field.On<TwoWayRelationshipModel>.Backing(x => x.Reference1);
		public static Field<ReferenceModel> Reference2Field = Field.On<TwoWayRelationshipModel>.Backing(x => x.Reference2);

		public ReferenceModel Reference1 { get { return GetValue(Reference1Field); } }
		public ReferenceModel Reference2 { get { return GetValue(Reference2Field); } }
	}
}