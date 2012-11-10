using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class FormDisallowedPreviewEvent : Event
	{
		public readonly Guid FormId;

		public FormDisallowedPreviewEvent(Guid formId)
		{
			FormId = formId;
		}
	}
}