using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using Cloak.Autofac;
using Cloak.Http.Media;
using Cloak.Web.Http.Autofac;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Hypermedia.Server;
using Grasp.Lists;

namespace Slate.Http.Server.Composition.Api
{
	public class ListModule : BuilderModule
	{
		public ListModule(HttpConfiguration httpSettings)
		{
			Contract.Requires(httpSettings != null);

			httpSettings.RegisterMediaFormat<HyperlistHtmlFormat>(this);

			httpSettings.ParameterBindingRules.Add(typeof(EntityId), parameter => parameter.BindWithModelBinding(new EntityIdBinder()));
			httpSettings.ParameterBindingRules.Add(typeof(ListViewKey), parameter => parameter.BindWithModelBinding(new ListViewKeyBinder()));
		}
	}
}