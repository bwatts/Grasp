using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using Cloak.Autofac;
using Cloak.Http.Media;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Hypermedia.Server;
using Grasp.Lists;

namespace Slate.Http.Server.Composition
{
	public class ListModule : BuilderModule
	{
		public ListModule(HttpConfiguration httpSettings)
		{
			Contract.Requires(httpSettings != null);

			var listFormat = new HyperlistHtmlFormat();

			httpSettings.Formatters.Add(listFormat);

			RegisterInstance(listFormat).As<MediaFormat>();

			httpSettings.ParameterBindingRules.Add(typeof(ListPageKey), parameter => parameter.BindWithModelBinding(new ListPageKeyBinder()));
		}
	}
}