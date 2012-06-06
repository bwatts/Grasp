using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Relationships
{
	public class OneWayRelationshipModel : Notion
	{
		public static Field<ReferenceModel> ReferenceField = Field.On<OneWayRelationshipModel>.Backing(x => x.Reference);

		public ReferenceModel Reference { get { return GetValue(ReferenceField); } }
	}
}