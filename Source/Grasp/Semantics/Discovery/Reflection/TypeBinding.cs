using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public abstract class TypeBinding : Notion
	{
		public static readonly Field<Type> TypeField = Field.On<TypeBinding>.For(x => x.Type);

		protected TypeBinding(Type type)
		{
			Contract.Requires(type != null);

			Type = type;
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		public override string ToString()
		{
			return Type.ToString();
		}

		public abstract TypeModel GetTypeModel();
	}
}