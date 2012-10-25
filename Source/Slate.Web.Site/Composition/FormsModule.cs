using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Cloak.Autofac;
using Grasp.Hypermedia.Lists;
using Slate.Web.Presentation.Lists;

namespace Slate.Web.Site.Composition
{
	public class FormsModule : BuilderModule
	{
		public FormsModule(string emptyListMessage)
		{
			RegisterType<ListClient>().InstancePerDependency();

			//Register(c => new ListFactory(c.ResolveNamed<IListService>("Forms"), c.ResolveNamed<IListMesh>("Forms"), emptyListMessage))
			//.Named<IListFactory>("Forms")
			//.InstancePerDependency();
		}
	}
}