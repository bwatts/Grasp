using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate
{
	public class FormCreatedEvent : Event
	{
		public static readonly Field<Guid> FormIdField = Field.On<FormCreatedEvent>.For(x => x.FormId);
		public static readonly Field<string> NameField = Field.On<FormCreatedEvent>.For(x => x.Name);

		public FormCreatedEvent(Guid formId, string name)
		{
			FormId = formId;
			Name = name;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}