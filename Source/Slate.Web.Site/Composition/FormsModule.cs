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
using Slate.Web.Site.Presentation.Explore;
using Slate.Web.Site.Presentation.Explore.Forms;
using Slate.Web.Site.Presentation.Lists;

namespace Slate.Web.Site.Composition
{
	public class FormsModule : BuilderModule
	{
		public FormsModule(RouteCollection routes, string emptyListMessage)
		{
			Contract.Requires(routes != null);

			Register(c => new ListMesh(
				itemCountTemplate: new Hyperlink("explore/forms", "{total-items}", "Explore the forms in your system"),
				pageTemplate: new Hyperlink("explore/forms", "{page}", "Page {page} of {page-count}"),
				itemTemplate: new Hyperlink("explore/forms/{id-escaped}", "{id}", "{id}"),
				itemNumberTemplate: new Hyperlink("explore/forms/{id-escaped}", "{item}", "{id}"),
				itemIdSelector: item => item["Name"]))
			.Named<IListMesh>("Forms")
			.SingleInstance();

			Register(c => new ListModelFactory(c.Resolve<IListClient>(), c.ResolveNamed<IListMesh>("Forms"), emptyListMessage))
			.Named<IListModelFactory>("Forms")
			.InstancePerDependency();

			RegisterType<FormService>().As<IFormService>().InstancePerDependency();

			RegisterType<FormsController>().InstancePerDependency();

			routes.MapRoute("form-start", "explore/forms/start", new { controller = "Forms", action = "Start" });
			routes.MapRoute("form-list", "explore/forms", new { controller = "Forms", action = "List" });
			routes.MapRoute("form-details", "explore/forms/{id}", new { controller = "Forms", action = "Details" });
		}
	}
}