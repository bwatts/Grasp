using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate
{
	public class FormDisallowedPreviewEvent : Event
	{
		public static readonly Field<Guid> FormIdField = Field.On<FormDisallowedPreviewEvent>.For(x => x.FormId);

		public FormDisallowedPreviewEvent(Guid formId)
		{
			FormId = formId;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}