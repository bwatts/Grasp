using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class ReportProgressCommand : Command
	{
		public static readonly Field<Progress> ProgressField = Field.On<ReportProgressCommand>.For(x => x.Progress);
		public static readonly Field<FullName> TopicNameField = Field.On<ReportProgressCommand>.For(x => x.TopicName);

		public ReportProgressCommand(FullName workItemName, Progress progress) : base(workItemName)
		{
			Contract.Requires(progress > Progress.Accepted && progress < Progress.Complete);

			Progress = progress;
		}

		public ReportProgressCommand(FullName workItemName, FullName topicName) : base(workItemName)
		{
			Contract.Requires(topicName != null);

			Progress = Progress.Complete;
			TopicName = topicName;
		}

		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public FullName TopicName { get { return GetValue(TopicNameField); } private set { SetValue(TopicNameField, value); } }
	}
}