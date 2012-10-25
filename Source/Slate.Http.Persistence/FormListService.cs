using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Hypermedia.Lists;
using Grasp.Raven;
using Grasp.Work;
using Raven.Client;
using Raven.Client.Linq;

namespace Slate.Http.Persistence
{
	public class FormListService : RavenListService<Form>
	{
		public FormListService(IDocumentStore documentStore) : base(documentStore)
		{}

		protected override IRavenQueryable<Form> Query(IDocumentSession session)
		{
			return session.Query<Form>();
		}

		protected override IReadOnlyDictionary<string, object> SelectValues(ListPageContext context, Form item)
		{
			return AnonymousDictionary.Read(new
			{
				Name = item.Name,
				WhenCreated = EntityLifetime.WhenCreatedField.Get(item),
				WhenModified = EntityLifetime.WhenModifiedField.Get(item),
				Visibility = item.Visibility,
				ResponseCount = 4,
				IssueCount = 2
			});
		}
	}
}