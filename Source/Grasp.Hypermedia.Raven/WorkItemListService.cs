using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Reflection;
using Grasp.Lists;
using Grasp.Raven;
using Grasp.Work.Items;
using Raven.Client;
using Raven.Client.Linq;

namespace Grasp.Hypermedia.Raven
{
	public class WorkItemListService : RavenListService<WorkItem>
	{
		public WorkItemListService(IDocumentStore documentStore) : base(documentStore)
		{}

		protected override IRavenQueryable<WorkItem> Query(IDocumentSession session)
		{
			return session.Query<WorkItem>();
		}

		protected override ListSchema GetSchema()
		{
			return new ListSchema(AnonymousDictionary.Read<Type>(new
			{
				Id = typeof(Guid),
				Description = typeof(string),
				RetryInterval = typeof(TimeSpan),
				Progress = typeof(Progress),
				ResultResource = typeof(Uri)
			}));
		}

		protected override ListItemBindings SelectBindings(ListPageContext context, WorkItem item)
		{
			return new ListItemBindings(AnonymousDictionary.Read(new
			{
				Id = item.Id,
				Description = item.Description,
				RetryInterval = item.RetryInterval,
				Progress = item.Progress,
				ResultResource = item.ResultLocation
			}));
		}
	}
}