using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Lists;
using Grasp.Raven;
using Grasp.Work;
using Raven.Client;
using Raven.Client.Linq;

namespace Slate.Http.Persistence
{
	public class IssueListService : RavenListService<Issue>
	{
		public IssueListService(IDocumentStore documentStore) : base(documentStore)
		{}

		protected override IRavenQueryable<Issue> Query(IDocumentSession session)
		{
			return session.Query<Issue>();
		}

		protected override ListSchema GetSchema()
		{
			return new ListSchema(AnonymousDictionary.Read<Type>(new
			{
				Number = typeof(int),
				Description = typeof(string),
				AssignedTo = typeof(string),
				WhenCreated = typeof(DateTime),
				WhenModified = typeof(DateTime)
			}));
		}

		protected override ListItemBindings SelectBindings(ListPageContext context, Issue item)
		{
			return new ListItemBindings(AnonymousDictionary.Read(new
			{
				Number = item.Number,
				Description = item.Title,
				AssignedTo = "TODO",
				WhenCreated = EntityLifetime.WhenCreatedField.Get(item),
				WhenModified = EntityLifetime.WhenModifiedField.Get(item)
			}));
		}
	}
}