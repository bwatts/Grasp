using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public sealed class ReportProgressHandler : Notion, IHandler<ReportProgressCommand>
	{
		public static readonly Field<IRepository<WorkItem>> _workItemRepositoryField = Field.On<ReportProgressHandler>.For(x => x._workItemRepository);

		private IRepository<WorkItem> _workItemRepository { get { return GetValue(_workItemRepositoryField); } set { SetValue(_workItemRepositoryField, value); } }

		public ReportProgressHandler(IRepository<WorkItem> workItemRepository)
		{
			Contract.Requires(workItemRepository != null);

			_workItemRepository = workItemRepository;
		}

		public async Task HandleAsync(ReportProgressCommand c)
		{
			var workItem = await _workItemRepository.LoadAsync(c.WorkItemId);

			workItem.Handle(c);

			await _workItemRepository.SaveAsync(workItem);
		}
	}
}