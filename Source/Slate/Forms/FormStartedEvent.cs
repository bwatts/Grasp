using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class FormStartedEvent : Event
	{
		public readonly Guid FormId;
		public readonly string Name;

		public FormStartedEvent(Guid formId, string name)
		{
			FormId = formId;
			Name = name;
		}
	}
}