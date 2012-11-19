using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Work.Items;

namespace Slate.Web.Site.Presentation.Work
{
	public class WorkItemModel : ViewModel
	{
		public static readonly Field<string> DescriptionField = Field.On<WorkItemModel>.For(x => x.Description);
		public static readonly Field<DateTime> WhenStartedField = Field.On<WorkItemModel>.For(x => x.WhenStarted);
		public static readonly Field<TimeSpan> AgeField = Field.On<WorkItemModel>.For(x => x.Age);
		public static readonly Field<Progress> ProgressField = Field.On<WorkItemModel>.For(x => x.Progress);
		public static readonly Field<int> RetryIntervalMillisecondsField = Field.On<WorkItemModel>.For(x => x.RetryIntervalMilliseconds);

		public WorkItemModel(string description, DateTime whenStarted, TimeSpan age, Progress progress, int retryIntervalMilliseconds)
		{
			Contract.Requires(description != null);
			Contract.Requires(age >= TimeSpan.Zero);
			Contract.Requires(retryIntervalMilliseconds >= 0);

			Description = description;
			WhenStarted = whenStarted;
			Age = age;
			Progress = progress;
			RetryIntervalMilliseconds = retryIntervalMilliseconds;
		}

		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public DateTime WhenStarted { get { return GetValue(WhenStartedField); } private set { SetValue(WhenStartedField, value); } }
		public TimeSpan Age { get { return GetValue(AgeField); } private set { SetValue(AgeField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public int RetryIntervalMilliseconds { get { return GetValue(RetryIntervalMillisecondsField); } private set { SetValue(RetryIntervalMillisecondsField, value); } }

		public bool Started
		{
			get { return Progress > Progress.Accepted; }
		}
	}
}