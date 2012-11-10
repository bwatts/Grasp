using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class FieldAttacherModel : TypeModel
	{
		public static readonly Field<Many<Field>> AttachedFieldsField = Field.On<FieldAttacherModel>.For(x => x.AttachedFields);

		public FieldAttacherModel(Type type, Many<Field> attachedFields) : base(type)
		{
			Contract.Requires(attachedFields != null);
			Contract.ForAll(attachedFields, attachedField => attachedField.IsAttached);

			AttachedFields = attachedFields;
		}

		public Many<Field> AttachedFields { get { return GetValue(AttachedFieldsField); } private set { SetValue(AttachedFieldsField, value); } }
	}
}