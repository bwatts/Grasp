using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Work;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class NotionBinding : TypeBinding
	{
		public static readonly Field<Many<FieldBinding>> FieldBindingsField = Field.On<NotionBinding>.For(x => x.FieldBindings);

		public NotionBinding(Type type, IEnumerable<FieldBinding> fieldBindings) : base(type)
		{
			Contract.Requires(fieldBindings != null);

			FieldBindings = fieldBindings.ToMany();
		}

		public Many<FieldBinding> FieldBindings { get { return GetValue(FieldBindingsField); } private set { SetValue(FieldBindingsField, value); } }

		public override TypeModel GetTypeModel()
		{
			var fieldsByIsAttached = FieldBindings.ToLookup(binding => binding.Field.IsAttached);

			var fields = fieldsByIsAttached[false].Select(field => field.Field).ToMany();
			var attachedFields = fieldsByIsAttached[true].Select(attachedField => attachedField.Field).ToMany();

			return typeof(IAggregate).IsAssignableFrom(Type)
				? new AggregateModel(Type, attachedFields, fields)
				: new NotionModel(Type, attachedFields, fields);
		}
	}
}