using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class NamespaceModel : Notion
	{
		public static readonly Field<string> NameField = Field.On<NamespaceModel>.For(x => x.Name);
		public static readonly Field<Many<TypeModel>> TypesField = Field.On<NamespaceModel>.For(x => x.Types);

		public string Name { get { return GetValue(NameField); } }
		public Many<TypeModel> Types { get { return GetValue(TypesField); } }

		public override string ToString()
		{
			return Name;
		}
	}
}