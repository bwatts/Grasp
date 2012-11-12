using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Items;
using Slate.Forms;

namespace Slate.Services
{
	public sealed class StartFormService : Publisher, IStartFormService
	{
		public static readonly Field<TimeSpan> _retryIntervalField = Field.On<StartFormService>.For(x => x._retryInterval);

		private TimeSpan _retryInterval { get { return GetValue(_retryIntervalField); } set { SetValue(_retryIntervalField, value); } }

		public StartFormService(TimeSpan retryInterval)
		{
			Contract.Requires(retryInterval >= TimeSpan.Zero);

			_retryInterval = retryInterval;
		}

		public async Task<WorkItem> StartFormAsync(string name)
		{
			var formId = Guid.NewGuid();

			await IssueAsync(new StartFormCommand(formId, name));

			return new WorkItem(formId, "Start form", _retryInterval);
		}
	}
}