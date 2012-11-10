using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class FormAllowedPreviewEvent : Event
	{
		public readonly Guid FormId;

		public FormAllowedPreviewEvent(Guid formId)
		{
			FormId = formId;
		}
	}
}