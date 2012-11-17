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
using Slate.Forms;

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

		protected override ListSchema GetSchema()
		{
			return new ListSchema(AnonymousDictionary.Read<Type>(new
			{
				Id = typeof(Guid),
				Name = typeof(string),
				Phase = typeof(string),
				ResponseCount = typeof(int),
				IssueCount = typeof(int),
				WhenCreated = typeof(DateTime),
				WhenModified = typeof(DateTime)
			}));
		}

		protected override ListItemBindings SelectBindings(ListPageContext context, Form item)
		{
			return new ListItemBindings(AnonymousDictionary.Read(new
			{
				Id = item.Id,
				Name = item.Name,
				Phase = FormPhase.Draft.ToString(),
				ResponseCount = 4,
				IssueCount = 2,
				WhenCreated = DateTime.Now.AddDays(-2),
				WhenModified = DateTime.Now.AddDays(-1)
			}));
		}
	}
}