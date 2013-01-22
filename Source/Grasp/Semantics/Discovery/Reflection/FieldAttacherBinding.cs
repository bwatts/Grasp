using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class FieldAttacherBinding : TypeBinding
	{
		public static readonly Field<Many<FieldBinding>> AttachedFieldBindingsField = Field.On<FieldAttacherBinding>.For(x => x.AttachedFieldBindings);

		public FieldAttacherBinding(Type type, Many<FieldBinding> attachedFieldBindings) : base(type)
		{
			Contract.Requires(attachedFieldBindings != null);
			Contract.ForAll(attachedFieldBindings, attachedFieldBinding => attachedFieldBinding.Field.IsAttached);

			AttachedFieldBindings = attachedFieldBindings;
		}

		public Many<FieldBinding> AttachedFieldBindings { get { return GetValue(AttachedFieldBindingsField); } private set { SetValue(AttachedFieldBindingsField, value); } }

		public override TypeModel GetTypeModel()
		{
			return new FieldAttacherModel(Type, AttachedFieldBindings.Select(attachedFieldBinding => attachedFieldBinding.Field).ToMany());
		}
	}
}