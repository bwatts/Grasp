using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class StartFormCommand : Command
	{
		public static readonly Field<Guid> IdField = Field.On<StartFormCommand>.For(x => x.Id);
		public static readonly Field<string> NameField = Field.On<StartFormCommand>.For(x => x.Name);

		public StartFormCommand(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public Guid Id { get { return GetValue(IdField); } private set { SetValue(IdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}