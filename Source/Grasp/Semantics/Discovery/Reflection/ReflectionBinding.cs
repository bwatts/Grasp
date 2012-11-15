using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class ReflectionBinding : Notion, IDomainModelBinding
	{
		public static readonly Field<string> NameField = Field.On<ReflectionBinding>.For(x => x.Name);
		public static readonly Field<Many<NamespaceBinding>> NamespaceBindingsField = Field.On<ReflectionBinding>.For(x => x.NamespaceBindings);

		public ReflectionBinding(string name, IEnumerable<NamespaceBinding> namespaceBindings)
		{
			Contract.Requires(name != null);
			Contract.Requires(namespaceBindings != null);

			Name = name;
			NamespaceBindings = namespaceBindings.ToMany();
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Many<NamespaceBinding> NamespaceBindings { get { return GetValue(NamespaceBindingsField); } private set { SetValue(NamespaceBindingsField, value); } }

		public DomainModel BindDomainModel()
		{
			var references = GetReferences().ToList();

			var relationships =
				from reference1 in references
				from reference2 in references
				where reference1.ReferencedNotion == reference2.ReferencingNotion
				select new
				{
					Reference1 = reference1,
					Reference2 = reference2,
					IsTwoWay = reference2.ReferencedNotion == reference1.ReferencedNotion
				};

			var relationshipsByIsTwoWay = relationships.ToLookup(relationship => relationship.IsTwoWay);

			return new DomainModel(
				Name,
				GetNamespaces(),
				relationshipsByIsTwoWay[false].Select(relationship => new OneWayRelationshipModel(relationship.Reference1)),
				relationshipsByIsTwoWay[true].Select(relationship => new TwoWayRelationshipModel(relationship.Reference1, relationship.Reference2)));
		}

		private IEnumerable<ReferenceModel> GetReferences()
		{
			return new ReferenceBuilder(GetNamespaces()).GetReferences();
		}

		private IEnumerable<NamespaceModel> GetNamespaces()
		{
			return NamespaceBindings.Select(namespaceBinding => namespaceBinding.GetModel());
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
					from notion in @namespace.Types.OfType<NotionModel>()
					from field in notion.Fields
					let reference = GetReference(notion, field)
					where reference != null
					select reference;
			}

			private ReferenceModel GetReference(NotionModel referencingNotion, Field referencingField)
			{
				var valueType = referencingField.ValueType;

				if(referencingField.AsMany.IsMany)
				{
					return GetManyReference(referencingNotion, referencingField);
				}
				else if(referencingField.AsNonMany.IsNonMany)
				{
					return GetNonManyReference(referencingNotion, referencingField);
				}
				else if(typeof(Notion).IsAssignableFrom(referencingField.ValueType))
				{
					return GetSingularReference(referencingNotion, referencingField);
				}
				else
				{
					return null;
				}
			}

			private ReferenceModel GetManyReference(NotionModel referencingNotion, Field referencingField)
			{
				var referencedNotion = GetReferencedNotion(referencingField);

				return referencedNotion == null ? null : new ReferenceModel(referencingNotion, referencingField, referencedNotion, GetPluralCardinality(referencingField));
			}

			private ReferenceModel GetNonManyReference(NotionModel referencingNotion, Field referencingField)
			{
				throw new NotImplementedException("Serialization of non-many fields is not complete");
			}

			private NotionModel GetReferencedNotion(Field pluralReferencingField)
			{
				var notionType = pluralReferencingField.ValueType.GetGenericArguments().Single();

				return GetReferencedNotion(notionType);
			}

			private NotionModel GetReferencedNotion(Type notionType)
			{
				return _namespaces
					.SelectMany(@namespace => @namespace.Types.OfType<NotionModel>())
					.Where(notion => notion.Type == notionType)
					.FirstOrDefault();
			}

			private static Cardinality GetPluralCardinality(Field referencingField)
			{
				// TODO: This isn't right
				return Cardinality.ZeroToMany;
			}

			private ReferenceModel GetSingularReference(NotionModel referencingNotion, Field referencingField)
			{
				var referencedNotion = GetReferencedNotion(referencingField.ValueType);

				return referencedNotion == null ? null : new ReferenceModel(referencingNotion, referencingField, referencedNotion, GetSingularCardinality(referencingField));
			}

			private static Cardinality GetSingularCardinality(Field referencingField)
			{
				// TODO: This isn't right
				return referencingField.IsNullable ? Cardinality.ZeroToOne : Cardinality.OneToOne;
			}
		}
	}
}