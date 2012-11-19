using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class TestingStartedEvent : Event
	{
		public static readonly Field<EntityId> FormIdField = Field.On<TestingStartedEvent>.For(x => x.FormId);

		public TestingStartedEvent(EntityId formId)
		{
			FormId = formId;
		}

		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}