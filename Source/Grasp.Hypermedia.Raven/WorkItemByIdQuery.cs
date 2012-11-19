using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Server;
using Grasp.Raven;
using Grasp.Work.Items;
using Raven.Client;

namespace Grasp.Hypermedia.Raven
{
	public sealed class WorkItemByIdQuery : RavenContext, IWorkItemByIdQuery
	{
		public WorkItemByIdQuery(IDocumentStore documentStore) : base(documentStore)
		{}

		public Task<WorkItem> GetWorkItemAsync(EntityId id)
		{
			return ExecuteReadAsync(session => session.Load<WorkItem>("WorkItems/" + id.ToString()));
		}
	}
}