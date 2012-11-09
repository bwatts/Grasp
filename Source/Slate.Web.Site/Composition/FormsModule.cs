using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Slate.Web.Presentation.Lists;

namespace Slate.Web.Site.Composition
{
	public class FormsModule : BuilderModule
	{
		public FormsModule(string emptyListMessage)
		{
			Register(c => new ListMesh(
				countTemplate: new Hyperlink("explore/forms", "{item-count}", "Explore the forms in your system"),
				pageTemplate: new Hyperlink("explore/forms", "{page}", "Page {page} of {page-count}"),
				itemTemplate: new Hyperlink("explore/forms/{id-escaped}", "{id}", "{id}"),
				itemIdSelector: item => item["Name"]))
			.Named<IListMesh>("Forms")
			.SingleInstance();

			Register(c => new ListModelFactory(c.Resolve<IListClient>(), c.ResolveNamed<IListMesh>("Forms"), emptyListMessage))
			.Named<IListModelFactory>("Forms")
			.InstancePerDependency();
		}
	}
}