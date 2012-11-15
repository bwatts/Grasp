using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class DraftResumedEvent : Event
	{
		public static readonly Field<Guid> FormIdField = Field.On<DraftResumedEvent>.For(x => x.FormId);

		public DraftResumedEvent(Guid formId)
		{
			FormId = formId;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}