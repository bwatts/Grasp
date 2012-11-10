using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class NamespaceModel : Notion
	{
		public static readonly Field<string> NameField = Field.On<NamespaceModel>.For(x => x.Name);
		public static readonly Field<Many<TypeModel>> TypesField = Field.On<NamespaceModel>.For(x => x.Types);

		public NamespaceModel(string name, IEnumerable<TypeModel> types)
		{
			Contract.Requires(name != null);
			Contract.Requires(types != null);

			Name = name;
			Types = types.ToMany();
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Many<TypeModel> Types { get { return GetValue(TypesField); } private set { SetValue(TypesField, value); } }

		public override string ToString()
		{
			return Name;
		}

		public TypeModel GetTypeModel(Type type)
		{
			Contract.Requires(type != null);

			return Types.FirstOrDefault(namespaceType => namespaceType.Type == type);
		}
	}
}