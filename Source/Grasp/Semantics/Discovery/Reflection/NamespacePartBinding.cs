using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class NamespacePartBinding : Notion
	{
		public static Field<Assembly> AssemblyField = Field.On<NamespacePartBinding>.Backing(x => x.Assembly);
		public static Field<Many<TypeBinding>> TypeBindingsField = Field.On<NamespacePartBinding>.Backing(x => x.TypeBindings);

		public Assembly Assembly { get { return GetValue(AssemblyField); } }
		public Many<TypeBinding> TypeBindings { get { return GetValue(TypeBindingsField); } }

		public override string ToString()
		{
			return Assembly.ToString();
		}

		public IEnumerable<TypeModel> GetTypeModels()
		{
			return TypeBindings.Select(typeBinding => typeBinding.GetTypeModel());
		}
	}
}