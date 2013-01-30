using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkTimedOutEvent : WorkItemEvent
	{
		public static readonly Field<TimeSpan> TimeoutField = Field.On<WorkTimedOutEvent>.For(x => x.Timeout);
		public static readonly Field<TimeSpan> AgeField = Field.On<WorkTimedOutEvent>.For(x => x.Age);

		public WorkTimedOutEvent(FullName workItemName, TimeSpan timeout, TimeSpan age) : base(workItemName)
		{
			Contract.Requires(Check.That(timeout).IsInfiniteOrPositive());
			Contract.Requires(Check.That(age).IsInfiniteOrPositive());

			Timeout = timeout;
			Age = age;
		}

		public TimeSpan Timeout { get { return GetValue(TimeoutField); } private set { SetValue(TimeoutField, value); } }
		public TimeSpan Age { get { return GetValue(AgeField); } private set { SetValue(AgeField, value); } }
	}
}