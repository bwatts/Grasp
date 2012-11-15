using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class NotionModel : FieldAttacherModel
	{
		public static readonly Field<Many<Field>> FieldsField = Field.On<NotionModel>.For(x => x.Fields);

		public NotionModel(Type type, Many<Field> attachedFields, Many<Field> fields) : base(type, attachedFields)
		{
			Contract.Requires(typeof(Notion).IsAssignableFrom(type));
			Contract.Requires(fields != null);
			Contract.ForAll(fields, field => !field.IsAttached);

			Fields = fields;
		}

		public Many<Field> Fields { get { return GetValue(FieldsField); } private set { SetValue(FieldsField, value); } }

		public IEnumerable<Field> GetAttachableFields(DomainModel domainModel)
		{
			Contract.Requires(domainModel != null);

			return
				from @namespace in domainModel.Namespaces
				from type in @namespace.Types
				let fieldAttacher = type as FieldAttacherModel
				where fieldAttacher != null
				from attachedField in fieldAttacher.AttachedFields
				where attachedField.TargetType.IsAssignableFrom(Type)
				select attachedField;
		}

		public IEnumerable<Field> GetManyFields(DomainModel domainModel)
		{
			Contract.Requires(domainModel != null);

			return GetCollectionFields(domainModel, field => field.AsMany.IsMany);
		}

		public IEnumerable<Field> GetNonManyFields(DomainModel domainModel)
		{
			Contract.Requires(domainModel != null);

			return GetCollectionFields(domainModel, field => field.AsNonMany.IsNonMany);
		}

		private IEnumerable<Field> GetCollectionFields(DomainModel domainModel, Func<Field, bool> isCollectionPredicate)
		{
			return Fields.Concat(GetAttachableFields(domainModel)).Where(isCollectionPredicate);
		}
	}
}