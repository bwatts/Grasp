using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;
using Grasp.Knowledge.Persistence;

namespace Grasp.Semantics
{
	public abstract class TypeModel : Notion
	{
		public static Field<Type> TypeField = Field.On<TypeModel>.Backing(x => x.Type);

		public Type Type { get { return GetValue(TypeField); } }

		public override string ToString()
		{
			return Type.ToString();
		}
	}
}