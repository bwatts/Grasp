using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using Cloak.Autofac;
using Cloak.Http.Media;
using Grasp.Hypermedia.Lists;

namespace Slate.Http.Server.Composition
{
	public class ListModule : BuilderModule
	{
		public ListModule(HttpConfiguration httpConfiguration)
		{
			Contract.Requires(httpConfiguration != null);

			var listHtmlFormat = new ListHtmlFormat();

			httpConfiguration.Formatters.Add(listHtmlFormat);

			RegisterInstance(listHtmlFormat).As<MediaFormat>();

			httpConfiguration.ParameterBindingRules.Add(typeof(ListPageKey), parameter => parameter.BindWithModelBinding(new ListPageKeyBinder()));
		}
	}
}