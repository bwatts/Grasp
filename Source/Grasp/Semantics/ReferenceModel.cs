using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class ReferenceModel : Notion
	{
		public static readonly Field<NotionModel> ReferencingNotionField = Field.On<ReferenceModel>.For(x => x.ReferencingNotion);
		public static readonly Field<Field> ReferencingFieldField = Field.On<ReferenceModel>.For(x => x.ReferencingField);
		public static readonly Field<NotionModel> ReferencedNotionField = Field.On<ReferenceModel>.For(x => x.ReferencedNotion);
		public static readonly Field<Cardinality> CardinalityField = Field.On<ReferenceModel>.For(x => x.Cardinality);

		public ReferenceModel(NotionModel referencingNotion, Field referencingField, NotionModel referencedNotion, Cardinality cardinality)
		{
			Contract.Requires(referencingNotion != null);
			Contract.Requires(referencingField != null);
			Contract.Requires(referencedNotion != null);

			ReferencingNotion = referencingNotion;
			ReferencingField = referencingField;
			ReferencedNotion = referencedNotion;
			Cardinality = cardinality;
		}

		public NotionModel ReferencingNotion { get { return GetValue(ReferencingNotionField); } private set { SetValue(ReferencingNotionField, value); } }
		public Field ReferencingField { get { return GetValue(ReferencingFieldField); } private set { SetValue(ReferencingFieldField, value); } }
		public NotionModel ReferencedNotion { get { return GetValue(ReferencedNotionField); } private set { SetValue(ReferencedNotionField, value); } }
		public Cardinality Cardinality { get { return GetValue(CardinalityField); } private set { SetValue(CardinalityField, value); } }
	}
}