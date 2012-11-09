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

namespace Slate.Web.Site.Composition
{
	public class ListModule : BuilderModule
	{
		public ListModule(ModelBinderDictionary binders, HttpConfiguration httpConfiguration)
		{
			Contract.Requires(binders != null);
			Contract.Requires(httpConfiguration != null);

			binders.Add(typeof(ListPageKey), new Slate.Web.Site.Presentation.Lists.ListPageKeyBinder());

			var listFormat = new HyperlistHtmlFormat();

			RegisterInstance(listFormat).As<MediaFormat>();

			httpConfiguration.Formatters.Add(listFormat);

			RegisterType<ListClient>().As<IListClient>().InstancePerDependency();
		}
	}
}