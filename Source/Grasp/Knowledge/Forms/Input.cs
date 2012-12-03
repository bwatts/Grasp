using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Forms
{
	public abstract class Input : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<Input>.For(x => x.Name);

		protected Input(FullName name = null)
		{
			Name = name ?? FullName.Anonymous;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public abstract Schema GetSchema(Namespace rootNamespace);
	}
}