using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class OneWayRelationshipModel : Notion
	{
		public static readonly Field<ReferenceModel> ReferenceField = Field.On<OneWayRelationshipModel>.For(x => x.Reference);

		public OneWayRelationshipModel(ReferenceModel reference)
		{
			Contract.Requires(reference != null);

			Reference = reference;
		}

		public ReferenceModel Reference { get { return GetValue(ReferenceField); } private set { SetValue(ReferenceField, value); } }
	}
}