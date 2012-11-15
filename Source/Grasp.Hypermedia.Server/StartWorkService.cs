using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work.Items;

namespace Grasp.Hypermedia.Server
{
	public sealed class StartWorkService : Publisher, IStartWorkService
	{
		public static readonly Field<IHttpResourceContext> _resourceContextField = Field.On<StartWorkService>.For(x => x._resourceContext);

		private IHttpResourceContext _resourceContext { get { return GetValue(_resourceContextField); } set { SetValue(_resourceContextField, value); } }

		public StartWorkService(IHttpResourceContext resourceContext)
		{
			Contract.Requires(resourceContext != null);

			_resourceContext = resourceContext;
		}

		public async Task<Uri> StartWorkAsync(string description, TimeSpan retryInterval, Func<Guid, IMessageChannel, Task> issueWorkCommandAsync)
		{
			var workItemId = Guid.NewGuid();

			var messageChannel = GetMessageChannel();

			await messageChannel.IssueAsync(new StartWorkCommand(workItemId, description, retryInterval));

			await issueWorkCommandAsync(workItemId, messageChannel);

			return _resourceContext.GetAbsoluteUrl("work/" + workItemId.ToString("N").ToUpper());
		}
	}
}