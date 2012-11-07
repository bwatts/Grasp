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
				Visibility = typeof(FormVisibility),
				ResponseCount = typeof(int),
				IssueCount = typeof(int)
			}));
		}

		protected override ListItemBindings SelectBindings(ListPageContext context, Form item)
		{
			return new ListItemBindings(AnonymousDictionary.Read(new
			{
				Name = item.Name,
				WhenCreated = EntityLifetime.WhenCreatedField.Get(item),
				WhenModified = EntityLifetime.WhenModifiedField.Get(item),
				Visibility = item.Visibility,
				ResponseCount = 4,
				IssueCount = 2
			}));
		}
	}
}