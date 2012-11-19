using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class WentLiveEvent : Event
	{
		public static readonly Field<EntityId> FormIdField = Field.On<WentLiveEvent>.For(x => x.FormId);

		public WentLiveEvent(EntityId formId)
		{
			FormId = formId;
		}

		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}