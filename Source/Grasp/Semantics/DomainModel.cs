using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class DomainModel : NamedNotion
	{
		public static readonly Field<Many<NamespaceModel>> NamespacesField = Field.On<DomainModel>.For(x => x.Namespaces);
		public static readonly Field<Many<OneWayRelationshipModel>> OneWayRelationshipsField = Field.On<DomainModel>.For(x => x.OneWayRelationships);
		public static readonly Field<Many<TwoWayRelationshipModel>> TwoWayRelationshipsField = Field.On<DomainModel>.For(x => x.TwoWayRelationships);

		public DomainModel(
			FullName name,
			IEnumerable<NamespaceModel> namespaces,
			IEnumerable<OneWayRelationshipModel> oneWayRelationships,
			IEnumerable<TwoWayRelationshipModel> twoWayRelationships)
			: base(name)
		{
			Contract.Requires(namespaces != null);
			Contract.Requires(oneWayRelationships != null);
			Contract.Requires(twoWayRelationships != null);

			Namespaces = namespaces.ToMany();
			OneWayRelationships = oneWayRelationships.ToMany();
			TwoWayRelationships = twoWayRelationships.ToMany();
		}

		public Many<NamespaceModel> Namespaces { get { return GetValue(NamespacesField); } private set { SetValue(NamespacesField, value); } }
		public Many<OneWayRelationshipModel> OneWayRelationships { get { return GetValue(OneWayRelationshipsField); } private set { SetValue(OneWayRelationshipsField, value); } }
		public Many<TwoWayRelationshipModel> TwoWayRelationships { get { return GetValue(TwoWayRelationshipsField); } private set { SetValue(TwoWayRelationshipsField, value); } }

		public override string ToString()
		{
			return Name.ToString();
		}

		public IEnumerable<OneWayRelationshipModel> GetOneWayRelationships(Field referencingField)
		{
			Contract.Requires(referencingField != null);

			return OneWayRelationships.Where(oneWayRelationship => oneWayRelationship.Reference.ReferencingField == referencingField);
		}

		public IEnumerable<TwoWayRelationshipModel> GetTwoWayRelationships(Field referencingField)
		{
			Contract.Requires(referencingField != null);

			return TwoWayRelationships.Where(relationship => relationship.Reference1.ReferencingField == referencingField || relationship.Reference2.ReferencingField == referencingField);
		}

		public TypeModel GetTypeModel(Type type)
		{
			Contract.Requires(type != null);

			return Namespaces.Select(@namespace => @namespace.GetTypeModel(type)).FirstOrDefault(typeModel => typeModel != null);
		}
	}
}