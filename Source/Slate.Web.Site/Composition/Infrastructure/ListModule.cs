using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Cloak.Autofac;
using Cloak.Http.Media;
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

			binders.Add(typeof(ListPageKey), new Slate.Web.Site.Presentation.Lists.ListPageKeyBinder());

			var listFormat = new HyperlistHtmlFormat();

			RegisterInstance(listFormat).As<MediaFormat>();

			httpSettings.Formatters.Add(listFormat);

			RegisterType<ListClient>().As<IListClient>().InstancePerDependency();
		}
	}
}