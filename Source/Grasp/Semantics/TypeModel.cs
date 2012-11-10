using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public abstract class TypeModel : Notion
	{
		public static readonly Field<Type> TypeField = Field.On<TypeModel>.For(x => x.Type);

		protected TypeModel(Type type)
		{
			Contract.Requires(type != null);

			Type = type;
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		public override string ToString()
		{
			return Type.ToString();
		}
	}
}