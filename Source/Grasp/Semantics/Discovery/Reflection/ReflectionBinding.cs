using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Grasp.Semantics.Relationships;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class ReflectionBinding : DomainModelBinding
	{
		public static readonly Field<Many<NamespaceBinding>> NamespaceBindingsField = Field.On<ReflectionBinding>.For(x => x.NamespaceBindings);

		public Many<NamespaceBinding> NamespaceBindings { get { return GetValue(NamespaceBindingsField); } }

		public override DomainModel GetDomainModel()
		{
			var x = new DomainModel();

			var namespaces = GetNamespaces().ToList().AsReadOnly();

			DomainModel.NameField.Set(x, "<| Reflected |>");
			DomainModel.NamespacesField.Set(x, new Many<NamespaceModel>(namespaces));
			
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

			DomainModel.OneWayRelationshipsField.Set(domainModel, new Many<OneWayRelationshipModel>(oneWayRelationships));
			DomainModel.TwoWayRelationshipsField.Set(domainModel, new Many<TwoWayRelationshipModel>(twoWayRelationships));
		}

		private static IEnumerable<ReferenceModel> GetReferences(IEnumerable<NamespaceModel> namespaces)
		{
			return new ReferenceBuilder(namespaces).GetReferences();
		}

		private static TwoWayRelationshipModel GetTwoWayRelationship(ReferenceModel reference1, ReferenceModel reference2)
		{
			var x = new TwoWayRelationshipModel();

			TwoWayRelationshipModel.Reference1Field.Set(x, reference1);
			TwoWayRelationshipModel.Reference2Field.Set(x, reference2);

			return x;
		}

		private static OneWayRelationshipModel GetOneWayRelationship(ReferenceModel reference)
		{
			var x = new OneWayRelationshipModel();

			OneWayRelationshipModel.ReferenceField.Set(x, reference);

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
					from @object in @namespace.Types.OfType<EntityModel>()
					from field in @object.Fields
					let reference = GetReference(@object, field)
					where reference != null
					select reference;
			}

			private ReferenceModel GetReference(EntityModel referencingObject, Field field)
			{
				var valueType = field.ValueType;

				if(valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Many<>))
				{
					return GetPluralReference(referencingObject, field);
				}
				else if(typeof(Notion).IsAssignableFrom(field.ValueType))
				{
					return GetSingularReference(referencingObject, field);
				}
				else
				{
					return null;
				}
			}

			private ReferenceModel GetSingularReference(EntityModel referencingObject, Field referencingField)
			{
				var x = new ReferenceModel();

				ReferenceModel.ReferencedEntityField.Set(x, GetReferencedEntity(referencingField.ValueType));
				ReferenceModel.CardinalityField.Set(x, GetSingularCardinality(referencingField));
				ReferenceModel.ReferencingObjectField.Set(x, referencingObject);
				ReferenceModel.ReferencingFieldField.Set(x, referencingField);

				return x;
			}

			private static Cardinality GetSingularCardinality(Field referencingField)
			{
				// TODO: This isn't right
				return referencingField.IsNullable ? Cardinality.ZeroToOne : Cardinality.OneToOne;
			}

			private ReferenceModel GetPluralReference(EntityModel referencingObject, Field referencingField)
			{
				var x = new ReferenceModel();

				ReferenceModel.ReferencedEntityField.Set(x, GetReferencedEntity(referencingField));
				ReferenceModel.CardinalityField.Set(x, GetPluralCardinality(referencingField));
				ReferenceModel.ReferencingObjectField.Set(x, referencingObject);
				ReferenceModel.ReferencingFieldField.Set(x, referencingField);

				return x;
			}

			private static Cardinality GetPluralCardinality(Field referencingField)
			{
				// TODO: This isn't right
				return Cardinality.ZeroToMany;
			}

			private EntityModel GetReferencedEntity(Field pluralReferencingField)
			{
				var entityType = pluralReferencingField.ValueType.GetGenericArguments().Single();

				return GetReferencedEntity(entityType);
			}

			private EntityModel GetReferencedEntity(Type entityType)
			{


				var x = _namespaces.SelectMany(@namespace => @namespace.Types.OfType<EntityModel>()).ToList();



				return _namespaces
					.SelectMany(@namespace => @namespace.Types.OfType<EntityModel>())
					.Where(entity => entity.Type == entityType)
					.Single();
			}
		}
	}
}