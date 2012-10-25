using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Work;

namespace Slate
{
	public class Issue : Aggregate
	{
		public static readonly Field<int> NumberField = Field.On<Issue>.For(x => x.Number);
		public static readonly Field<string> TitleField = Field.On<Issue>.For(x => x.Title);

		public Issue(Guid id, int number, string title) : base(id)
		{
			Contract.Requires(number >= 1);
			Contract.Requires(title != null);

			Announce(new IssueOpenedEvent(id, number, title));
		}

		public int Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }

		private void Handle(IssueOpenedEvent @event)
		{
			PersistentId.ValueField.Set(this, @event.IssueId);
			Number = @event.Number;
			Title = @event.Title;

			SetWhenCreated(@event.When);
			SetWhenModified(@event.When);
		}
	}
}