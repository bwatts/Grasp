using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public abstract class TypeBinding : Notion
	{
		public static readonly Field<Type> TypeField = Field.On<TypeBinding>.For(x => x.Type);

		public Type Type { get { return GetValue(TypeField); } }

		public override string ToString()
		{
			return Type.ToString();
		}

		public abstract TypeModel GetTypeModel();
	}
}