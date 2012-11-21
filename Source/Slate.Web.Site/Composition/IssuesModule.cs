using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Slate.Web.Site.Presentation.Lists;

namespace Slate.Web.Site.Composition
{
	public class IssuesModule : BuilderModule
	{
		public IssuesModule(string emptyListMessage)
		{
			Register(c => new ListMesh(
				itemCountTemplate: new Hyperlink("issues", "{total-items}", "Manage the issues in your system"),
				pageTemplate: new Hyperlink("issues", "{page}", "Page {page} of {page-count}"),
				itemTemplate: new Hyperlink("issues/{id-escaped}", "{id}", "{id}"),
				itemNumberTemplate: new Hyperlink("issues/{id-escaped}", "{item}", "{id}"),
				itemIdSelector: item => item["Number"],
				itemTextSelector: item => item["Description"]))
			.Named<IListMesh>("Issues")
			.SingleInstance();

			Register(c => new ListModelFactory(c.Resolve<IListClient>(), c.ResolveNamed<IListMesh>("Issues"), emptyListMessage))
			.Named<IListModelFactory>("Issues")
			.InstancePerDependency();
		}
	}
}