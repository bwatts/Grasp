using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Grasp.Knowledge;
using Grasp.Semantics.Relationships;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class ReflectionBinding : DomainModelBinding
	{
		public static Field<Many<NamespaceBinding>> NamespaceBindingsField = Field.On<ReflectionBinding>.Backing(x => x.NamespaceBindings);

		public Many<NamespaceBinding> NamespaceBindings { get { return GetValue(NamespaceBindingsField); } }

		public override DomainModel GetDomainModel()
		{
			var x = new DomainModel();

			var namespaces = GetNamespaces().ToList().AsReadOnly();

			x.SetValue(DomainModel.NameField, "<| Reflected |>");
			x.SetValue(DomainModel.NamespacesField, namespaces);
			
			SetRelationships(x, namespaces);

			return x;
		}

		private IEnumerable<NamespaceModel> GetNamespaces()
		{
			return NamespaceBindings.Select(namespaceBinding => namespaceBinding.GetModel());
		}

		private static void SetRelationships(DomainModel domainModel, IEnumerable<NamespaceModel> namespaces)
		{
			var references = GetReferences(namespaces).ToList();

			var relationships =
				from reference1 in references
				from reference2 in references
				where reference1.ReferencedEntity == reference2.ReferencingObject
				select new
				{
					Reference1 = reference1,
					Reference2 = reference2,
					IsTwoWay = reference2.ReferencedEntity == reference1.ReferencedEntity
				};

			var oneWayRelationships = new List<OneWayRelationshipModel>();
			var twoWayRelationships = new List<TwoWayRelationshipModel>();

			foreach(var relationship in relationships)
			{
				if(relationship.IsTwoWay)
				{
					twoWayRelationships.Add(GetTwoWayRelationship(relationship.Reference1, relationship.Reference2));
				}
				else
				{
					oneWayRelationships.Add(GetOneWayRelationship(relationship.Reference1));
				}
			}

			domainModel.SetValue(DomainModel.OneWayRelationshipsField, oneWayRelationships.AsReadOnly());
			domainModel.SetValue(DomainModel.TwoWayRelationshipsField, twoWayRelationships.AsReadOnly());
		}

		private static IEnumerable<ReferenceModel> GetReferences(IEnumerable<NamespaceModel> namespaces)
		{
			return new ReferenceBuilder(namespaces).GetReferences();
		}

		private static TwoWayRelationshipModel GetTwoWayRelationship(ReferenceModel reference1, ReferenceModel reference2)
		{
			var x = new TwoWayRelationshipModel();

			x.SetValue(TwoWayRelationshipModel.Reference1Field, reference1);
			x.SetValue(TwoWayRelationshipModel.Reference2Field, reference2);

			return x;
		}

		private static OneWayRelationshipModel GetOneWayRelationship(ReferenceModel reference)
		{
			var x = new OneWayRelationshipModel();

			x.SetValue(OneWayRelationshipModel.ReferenceField, reference);

			return x;
		}

		private sealed class ReferenceBuilder
		{
			private readonly IEnumerable<NamespaceModel> _namespaces;

			internal ReferenceBuilder(IEnumerable<NamespaceModel> namespaces)
			{
				_namespaces = namespaces;
			}

			internal IEnumerable<ReferenceModel> GetReferences()
			{
				return
					from @namespace in _namespaces
					from @object in @namespace.Types.OfType<ObjectModel>()
					from field in @object.Fields
					let reference = GetReference(@object, field)
					where reference != null
					select reference;
			}

			private ReferenceModel GetReference(ObjectModel referencingObject, Field field)
			{
				if(typeof(Notion).IsAssignableFrom(field.ValueType))
				{
					return GetSingularReference(referencingObject, field);
				}
				else if(typeof(IEnumerable<Notion>).IsAssignableFrom(field.ValueType))
				{
					return GetPluralReference(referencingObject, field);
				}
				else
				{
					return null;
				}
			}

			private ReferenceModel GetSingularReference(ObjectModel referencingObject, Field referencingField)
			{
				var x = new ReferenceModel();

				x.SetValue(ReferenceModel.ReferencedEntityField, GetReferencedEntity(referencingField.ValueType));
				x.SetValue(ReferenceModel.CardinalityField, GetSingularCardinality(referencingField));
				x.SetValue(ReferenceModel.ReferencingObjectField, referencingObject);
				x.SetValue(ReferenceModel.ReferencingFieldField, referencingField);

				return x;
			}

			private static Cardinality GetSingularCardinality(Field referencingField)
			{
				// TODO: This isn't right
				return referencingField.IsNullable ? Cardinality.ZeroToOne : Cardinality.OneToOne;
			}

			private ReferenceModel GetPluralReference(ObjectModel referencingObject, Field referencingField)
			{
				var x = new ReferenceModel();

				x.SetValue(ReferenceModel.ReferencedEntityField, GetReferencedEntity(referencingField));
				x.SetValue(ReferenceModel.CardinalityField, GetPluralCardinality(referencingField));
				x.SetValue(ReferenceModel.ReferencingObjectField, referencingObject);
				x.SetValue(ReferenceModel.ReferencingFieldField, referencingField);

				return x;
			}

			private static Cardinality GetPluralCardinality(Field referencingField)
			{
				// TODO: This isn't right
				return Cardinality.ZeroToMany;
			}

			private ObjectModel GetReferencedEntity(Field pluralReferencingField)
			{
				var entityType = pluralReferencingField.ValueType.GetGenericArguments().Single();

				return GetReferencedEntity(entityType);
			}

			private ObjectModel GetReferencedEntity(Type entityType)
			{
				return _namespaces
					.SelectMany(@namespace => @namespace.Types.OfType<ObjectModel>())
					.Where(@object => @object.Type == entityType)
					.Single();
			}
		}
	}
}