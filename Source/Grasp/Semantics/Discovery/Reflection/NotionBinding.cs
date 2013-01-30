using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Work;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class NotionBinding : ClassBinding
	{
		public static readonly Field<Many<FieldBinding>> FieldBindingsField = Field.On<NotionBinding>.For(x => x.FieldBindings);

		public NotionBinding(Type type, IEnumerable<TraitBinding> traitBindings = null, IEnumerable<FieldBinding> fieldBindings = null) : base(type, traitBindings)
		{
			Contract.Requires(typeof(Notion).IsAssignableFrom(type));

			FieldBindings = (fieldBindings ?? Enumerable.Empty<FieldBinding>()).ToMany();
		}

		public Many<FieldBinding> FieldBindings { get { return GetValue(FieldBindingsField); } private set { SetValue(FieldBindingsField, value); } }

		protected override ClassModel GetClassModel(IEnumerable<Trait> traits)
		{
			return GetNotionModel(traits, GetFields());
		}

		protected virtual NotionModel GetNotionModel(IEnumerable<Trait> traits, IEnumerable<Field> fields)
		{
			return new NotionModel(Type, fields.ToMany(), traits);
		}

		private Many<Field> GetFields()
		{
			return FieldBindings.Select(fieldBinding => fieldBinding.Field).Where(field => field.Trait == null).ToMany();
		}
	}
}