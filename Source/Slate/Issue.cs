﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Work;
using Grasp.Work.Persistence;

namespace Slate
{
	public class Issue : Aggregate
	{
		public static readonly Field<int> NumberField = Field.On<Issue>.For(x => x.Number);
		public static readonly Field<string> TitleField = Field.On<Issue>.For(x => x.Title);

		public Issue(Guid id, int number, string title)
		{
			Contract.Requires(number >= 1);
			Contract.Requires(title != null);

			Announce(new IssueOpenedEvent(id, number, title));
		}

		public int Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }

		private void Handle(IssueOpenedEvent e)
		{
			SetValue(PersistentId.ValueField, e.IssueId);

			Number = e.Number;
			Title = e.Title;

			SetWhenCreated(e.When);
			SetWhenModified(e.When);
		}
	}
}