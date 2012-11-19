using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Items;

namespace Grasp.Hypermedia.Server
{
	public sealed class StartWorkService : Publisher, IStartWorkService
	{
		public static readonly Field<IRepository<WorkItem>> _workItemRepositoryField = Field.On<StartWorkService>.For(x => x._workItemRepository);

		private IRepository<WorkItem> _workItemRepository { get { return GetValue(_workItemRepositoryField); } set { SetValue(_workItemRepositoryField, value); } }

		public StartWorkService(IRepository<WorkItem> workItemRepository)
		{
			Contract.Requires(workItemRepository != null);

			_workItemRepository = workItemRepository;
		}

		public async Task<EntityId> StartWorkAsync(string description, TimeSpan retryInterval, Func<EntityId, IMessageChannel, Task> issueCommandAsync)
		{
			var id = EntityId.Generate();

			var workItem = new WorkItem(id, description, retryInterval);

			await _workItemRepository.SaveAsync(workItem);

			await issueCommandAsync(id, GetMessageChannel());

			return id;
		}
	}
}