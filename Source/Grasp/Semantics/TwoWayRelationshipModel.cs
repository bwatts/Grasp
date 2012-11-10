using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class TwoWayRelationshipModel : Notion
	{
		public static readonly Field<ReferenceModel> Reference1Field = Field.On<TwoWayRelationshipModel>.For(x => x.Reference1);
		public static readonly Field<ReferenceModel> Reference2Field = Field.On<TwoWayRelationshipModel>.For(x => x.Reference2);

		public TwoWayRelationshipModel(ReferenceModel reference1, ReferenceModel reference2)
		{
			Contract.Requires(reference1 != null);
			Contract.Requires(reference2 != null);

			Reference1 = reference1;
			Reference2 = reference2;
		}

		public ReferenceModel Reference1 { get { return GetValue(Reference1Field); } private set { SetValue(Reference1Field, value); } }
		public ReferenceModel Reference2 { get { return GetValue(Reference2Field); } private set { SetValue(Reference2Field, value); } }
	}
}