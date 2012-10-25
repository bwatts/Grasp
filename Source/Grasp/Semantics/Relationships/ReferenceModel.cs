using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Relationships
{
	public class ReferenceModel : Notion
	{
		public static readonly Field<EntityModel> ReferencingObjectField = Field.On<ReferenceModel>.For(x => x.ReferencingObject);
		public static readonly Field<Field> ReferencingFieldField = Field.On<ReferenceModel>.For(x => x.ReferencingField);
		public static readonly Field<EntityModel> ReferencedEntityField = Field.On<ReferenceModel>.For(x => x.ReferencedEntity);
		public static readonly Field<Cardinality> CardinalityField = Field.On<ReferenceModel>.For(x => x.Cardinality);

		public EntityModel ReferencingObject { get { return GetValue(ReferencingObjectField); } }
		public Field ReferencingField { get { return GetValue(ReferencingFieldField); } }
		public EntityModel ReferencedEntity { get { return GetValue(ReferencedEntityField); } }
		public Cardinality Cardinality { get { return GetValue(CardinalityField); } }
	}
}