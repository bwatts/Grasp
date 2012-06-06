using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Relationships
{
	public class ReferenceModel : Notion
	{
		public static Field<ObjectModel> ReferencingObjectField = Field.On<ReferenceModel>.Backing(x => x.ReferencingObject);
		public static Field<Field> ReferencingFieldField = Field.On<ReferenceModel>.Backing(x => x.ReferencingField);
		public static Field<ObjectModel> ReferencedEntityField = Field.On<ReferenceModel>.Backing(x => x.ReferencedEntity);
		public static Field<Cardinality> CardinalityField = Field.On<ReferenceModel>.Backing(x => x.Cardinality);

		public ObjectModel ReferencingObject { get { return GetValue(ReferencingObjectField); } }
		public Field ReferencingField { get { return GetValue(ReferencingFieldField); } }
		public ObjectModel ReferencedEntity { get { return GetValue(ReferencedEntityField); } }
		public Cardinality Cardinality { get { return GetValue(CardinalityField); } }
	}
}