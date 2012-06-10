using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class ObjectBinding : TypeBinding
	{
		public static Field<Many<MemberBinding>> MemberBindingsField = Field.On<ObjectBinding>.Backing(x => x.MemberBindings);

		public Many<MemberBinding> MemberBindings { get { return GetValue(MemberBindingsField); } }

		public override TypeModel GetTypeModel()
		{
			return typeof(Notion).IsAssignableFrom(Type) ? GetObjectModel() : GetValueModel() as TypeModel;
		}

		private ObjectModel GetObjectModel()
		{
			var x = new ObjectModel();

			x.SetValue(ObjectModel.TypeField, Type);
			x.SetValue(ObjectModel.FieldsField, new Many<Field>(GetFields()));

			return x;
		}

		private ValueModel GetValueModel()
		{
			// TODO: Why don't structs add anything to the equation?

			var x = new ValueModel();

			x.SetValue(ObjectModel.TypeField, Type);

			return x;
		}

		private IEnumerable<Field> GetFields()
		{
			return MemberBindings.Select(memberBinding => memberBinding.Field);
		}
	}
}