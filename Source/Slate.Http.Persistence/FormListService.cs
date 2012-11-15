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
				Name = typeof(string),
				WhenCreated = typeof(DateTime),
				WhenModified = typeof(DateTime),
				Visibility = typeof(FormPhase),
				ResponseCount = typeof(int),
				IssueCount = typeof(int)
			}));
		}

		protected override ListItemBindings SelectBindings(ListPageContext context, Form item)
		{
			return new ListItemBindings(AnonymousDictionary.Read(new
			{
				Name = "Test",
				WhenCreated = DateTime.Now.AddDays(-2),
				WhenModified = DateTime.Now.AddDays(-1),
				Visibility = FormPhase.Draft,
				ResponseCount = 4,
				IssueCount = 2
			}));
		}
	}
}