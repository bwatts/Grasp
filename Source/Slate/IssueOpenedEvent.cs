using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate
{
	public class IssueOpenedEvent : Event
	{
		public static readonly Field<EntityId> IssueIdField = Field.On<IssueOpenedEvent>.For(x => x.IssueId);
		public static readonly Field<int> NumberField = Field.On<IssueOpenedEvent>.For(x => x.Number);
		public static readonly Field<string> TitleField = Field.On<IssueOpenedEvent>.For(x => x.Title);

		public IssueOpenedEvent(EntityId issueId, int number, string title)
		{
			Contract.Requires(issueId != EntityId.Unassigned);
			Contract.Requires(number >= 1);
			Contract.Requires(title != null);

			IssueId = issueId;
			Number = number;
			Title = title;
		}

		public EntityId IssueId { get { return GetValue(IssueIdField); } private set { SetValue(IssueIdField, value); } }
		public int Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
	}
}