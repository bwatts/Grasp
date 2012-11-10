using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class NamespaceBinding : Notion
	{
		public static readonly Field<string> NameField = Field.On<NamespaceBinding>.For(x => x.Name);
		public static readonly Field<Many<NamespacePartBinding>> PartBindingsField = Field.On<NamespaceBinding>.For(x => x.PartBindings);

		public NamespaceBinding(string name, IEnumerable<NamespacePartBinding> partBindings)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));
			Contract.Requires(partBindings != null);

			Name = name;
			PartBindings = partBindings.ToMany();
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Many<NamespacePartBinding> PartBindings { get { return GetValue(PartBindingsField); } private set { SetValue(PartBindingsField, value); } }

		public override string ToString()
		{
			return Name;
		}

		public NamespaceModel GetModel()
		{
			return new NamespaceModel(Name, PartBindings.SelectMany(partBinding => partBinding.GetTypeModels()));
		}
	}
}