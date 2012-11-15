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
	public class ResumeDraftCommand : Command
	{
		public static readonly Field<Guid> FormIdField = Field.On<ResumeDraftCommand>.For(x => x.FormId);

		public ResumeDraftCommand(Guid formId)
		{
			Contract.Requires(formId != Guid.Empty);

			FormId = formId;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}