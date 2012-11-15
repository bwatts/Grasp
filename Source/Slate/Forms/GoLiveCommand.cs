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
	public class GoLiveCommand : Command
	{
		public static readonly Field<Guid> FormIdField = Field.On<GoLiveCommand>.For(x => x.FormId);

		public GoLiveCommand(Guid formId)
		{
			Contract.Requires(formId != Guid.Empty);

			FormId = formId;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}