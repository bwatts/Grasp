using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Semantics.Relationships;

namespace Grasp.Semantics
{
	public class DomainModel : Notion
	{
		public static readonly Field<string> NameField = Field.On<DomainModel>.For(x => x.Name);
		public static readonly Field<Many<NamespaceModel>> NamespacesField = Field.On<DomainModel>.For(x => x.Namespaces);
		public static readonly Field<Many<OneWayRelationshipModel>> OneWayRelationshipsField = Field.On<DomainModel>.For(x => x.OneWayRelationships);
		public static readonly Field<Many<TwoWayRelationshipModel>> TwoWayRelationshipsField = Field.On<DomainModel>.For(x => x.TwoWayRelationships);

		public string Name { get { return GetValue(NameField); } }
		public Many<NamespaceModel> Namespaces { get { return GetValue(NamespacesField); } }
		public Many<OneWayRelationshipModel> OneWayRelationships { get { return GetValue(OneWayRelationshipsField); } }
		public Many<TwoWayRelationshipModel> TwoWayRelationships { get { return GetValue(TwoWayRelationshipsField); } }

		public override string ToString()
		{
			return Name;
		}

		public IEnumerable<OneWayRelationshipModel> GetOneWayRelationships(Field referencingField)
		{
			Contract.Requires(referencingField != null);

			return OneWayRelationships.Where(oneWayRelationship => oneWayRelationship.Reference.ReferencingField == referencingField);
		}

		public IEnumerable<TwoWayRelationshipModel> GetTwoWayRelationships(Field referencingField)
		{
			Contract.Requires(referencingField != null);

			return TwoWayRelationships.Where(twoWayRelationship =>
				twoWayRelationship.Reference1.ReferencingField == referencingField || twoWayRelationship.Reference2.ReferencingField == referencingField);
		}
	}
}