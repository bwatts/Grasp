using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Slate.Http.Persistence;

namespace Slate.Http.Server.Composition
{
	public class IssuesModule : BuilderModule
	{
		public IssuesModule()
		{
			//RegisterType<FormListService>().As<IListService<FormItem>>().InstancePerDependency();

			//Register(c => new ListMesh(
			//	itemNumberLinkTemplate: new HtmlLink("explore/issues?item={item}", "{item}", "Issue {item} of {total-items}"),
			//	totalLinkTemplate: new HtmlLink("explore/issues", "{total-items}", "Explore the issues in your system"),
			//	pageLinkTemplate: new HtmlLink("explore/issues?page={page}", "{page}", "Page {page} of {total-pages}"),
			//	itemPathTemplate: new HtmlLink("explore/issues/{id-escaped}", "{id}", "[issue] {id}")))
			//.Named<IListMesh>("Issues")
			//.SingleInstance();

			//Register(c => new ListFactory(c.ResolveNamed<IListMesh>("Issues")))
			//.Named<IListFactory>("Issues")
			//.SingleInstance();

			//Register(c => new IssueListFactory(c.Resolve<ITimeContext>(), c.Resolve<IListService<IssueItem>>(), c.ResolveKeyed<IListFactory>("Issues")))
			//.As<IIssueListFactory>()
			//.InstancePerDependency();
		}
	}
}