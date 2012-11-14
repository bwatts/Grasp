using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class StartFormCommand : Command
	{
		public static readonly Field<Guid> FormIdField = Field.On<StartFormCommand>.For(x => x.FormId);
		public static readonly Field<string> NameField = Field.On<StartFormCommand>.For(x => x.Name);

		public StartFormCommand(Guid formId, string name)
		{
			Contract.Requires(formId != Guid.Empty);
			Contract.Requires(name != null);

			FormId = formId;
			Name = name;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}