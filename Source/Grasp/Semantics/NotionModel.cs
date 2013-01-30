using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class NotionModel : ClassModel
	{
		public static readonly Field<Many<Field>> FieldsField = Field.On<NotionModel>.For(x => x.Fields);

		public NotionModel(Type type, Many<Field> fields, IEnumerable<Trait> traits = null) : base(type, traits)
		{
			Contract.Requires(typeof(Notion).IsAssignableFrom(type));
			Contract.Requires(fields != null);
			Contract.ForAll(fields, field => field.Trait == null);

			Fields = fields;
		}

		public Many<Field> Fields { get { return GetValue(FieldsField); } private set { SetValue(FieldsField, value); } }

		public IEnumerable<Field> GetTraitFields(DomainModel domain)
		{
			Contract.Requires(domain != null);

			return
				from @namespace in domain.Namespaces
				from @class in @namespace.Types.OfType<ClassModel>()
				from trait in @class.Traits
				from traitField in trait.Fields
				where traitField.TargetType.IsAssignableFrom(Type)
				select traitField;
		}

		public IEnumerable<Field> GetManyFields(DomainModel domain)
		{
			Contract.Requires(domain != null);

			return GetCollectionFields(domain, field => field.AsMany.IsMany);
		}

		public IEnumerable<Field> GetNonManyFields(DomainModel domain)
		{
			Contract.Requires(domain != null);

			return GetCollectionFields(domain, field => field.AsNonMany.IsNonMany);
		}

		private IEnumerable<Field> GetCollectionFields(DomainModel domain, Func<Field, bool> isCollectionPredicate)
		{
			return Fields.Concat(GetTraitFields(domain)).Where(isCollectionPredicate);
		}
	}
}