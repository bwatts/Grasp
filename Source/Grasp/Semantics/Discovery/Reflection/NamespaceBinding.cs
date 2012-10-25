using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class NamespaceBinding : Notion
	{
		public static readonly Field<string> NameField = Field.On<NamespaceBinding>.For(x => x.Name);
		public static readonly Field<Many<NamespacePartBinding>> PartBindingsField = Field.On<NamespaceBinding>.For(x => x.PartBindings);

		public string Name { get { return GetValue(NameField); } }
		public Many<NamespacePartBinding> PartBindings { get { return GetValue(PartBindingsField); } }

		public override string ToString()
		{
			return Name;
		}

		public NamespaceModel GetModel()
		{
			var x = new NamespaceModel();

			NamespaceModel.NameField.Set(x, Name);
			NamespaceModel.TypesField.Set(x, new Many<TypeModel>(GetTypeModels()));

			return x;
		}

		private IEnumerable<TypeModel> GetTypeModels()
		{
			return PartBindings.SelectMany(partBinding => partBinding.GetTypeModels());
		}
	}
}