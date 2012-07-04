using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Cloak.Autofac;

namespace Grasp.Hypermedia.Http.Composition
{
	public class HypermediaModule : BuilderModule
	{
		public HypermediaModule(params Assembly[] resourceAssemblies)
		{
			this.RegisterHttpResources(resourceAssemblies).InstancePerApiRequest();
		}
	}
}