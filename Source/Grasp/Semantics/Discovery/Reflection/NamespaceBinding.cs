using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class NamespaceBinding : Notion
	{
		public static Field<string> NameField = Field.On<NamespaceBinding>.Backing(x => x.Name);
		public static Field<Many<NamespacePartBinding>> PartBindingsField = Field.On<NamespaceBinding>.Backing(x => x.PartBindings);

		public string Name { get { return GetValue(NameField); } }
		public Many<NamespacePartBinding> PartBindings { get { return GetValue(PartBindingsField); } }

		public override string ToString()
		{
			return Name;
		}

		public NamespaceModel GetModel()
		{
			var x = new NamespaceModel();

			x.SetValue(NamespaceModel.NameField, Name);
			x.SetValue(NamespaceModel.TypesField, new Many<TypeModel>(GetTypeModels()));

			return x;
		}

		private IEnumerable<TypeModel> GetTypeModels()
		{
			return PartBindings.SelectMany(partBinding => partBinding.GetTypeModels());
		}
	}
}