using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class DomainModel : Notion
	{
		public static readonly Field<string> NameField = Field.On<DomainModel>.For(x => x.Name);
		public static readonly Field<Many<NamespaceModel>> NamespacesField = Field.On<DomainModel>.For(x => x.Namespaces);
		public static readonly Field<Many<OneWayRelationshipModel>> OneWayRelationshipsField = Field.On<DomainModel>.For(x => x.OneWayRelationships);
		public static readonly Field<Many<TwoWayRelationshipModel>> TwoWayRelationshipsField = Field.On<DomainModel>.For(x => x.TwoWayRelationships);

		public DomainModel(
			string name,
			IEnumerable<NamespaceModel> namespaces,
			IEnumerable<OneWayRelationshipModel> oneWayRelationships,
			IEnumerable<TwoWayRelationshipModel> twoWayRelationships)
		{
			Contract.Requires(name != null);
			Contract.Requires(namespaces != null);
			Contract.Requires(oneWayRelationships != null);
			Contract.Requires(twoWayRelationships != null);

			Name = name;
			Namespaces = namespaces.ToMany();
			OneWayRelationships = oneWayRelationships.ToMany();
			TwoWayRelationships = twoWayRelationships.ToMany();
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Many<NamespaceModel> Namespaces { get { return GetValue(NamespacesField); } private set { SetValue(NamespacesField, value); } }
		public Many<OneWayRelationshipModel> OneWayRelationships { get { return GetValue(OneWayRelationshipsField); } private set { SetValue(OneWayRelationshipsField, value); } }
		public Many<TwoWayRelationshipModel> TwoWayRelationships { get { return GetValue(TwoWayRelationshipsField); } private set { SetValue(TwoWayRelationshipsField, value); } }

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

			return TwoWayRelationships.Where(relationship => relationship.Reference1.ReferencingField == referencingField || relationship.Reference2.ReferencingField == referencingField);
		}

		public TypeModel GetTypeModel(Type type)
		{
			Contract.Requires(type != null);

			return Namespaces.Select(@namespace => @namespace.GetTypeModel(type)).FirstOrDefault(typeModel => typeModel != null);
		}
	}
}