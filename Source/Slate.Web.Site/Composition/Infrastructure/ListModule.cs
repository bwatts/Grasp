using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Cloak.Autofac;
using Cloak.Http.Media;
using Cloak.Web.Http.Autofac;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Composition.Infrastructure
{
	public class ListModule : BuilderModule
	{
		public ListModule(ModelBinderDictionary binders, HttpConfiguration httpSettings)
		{
			Contract.Requires(binders != null);
			Contract.Requires(httpSettings != null);

			binders.Add(typeof(EntityId), new Slate.Web.Site.Presentation.EntityIdBinder());
			binders.Add(typeof(ListViewKey), new Slate.Web.Site.Presentation.Lists.ListViewKeyBinder());

			httpSettings.RegisterMediaFormat<HyperlistHtmlFormat>(this);

			RegisterType<ListClient>().As<IListClient>().InstancePerDependency();
		}
	}
}