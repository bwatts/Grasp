using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Cloak.Http.Media;
using Grasp.Hypermedia.Lists;

namespace Slate.Web.Site.Composition.Http
{
	public class ListModule : BuilderModule
	{
		public ListModule()
		{
			RegisterType<ListHtmlFormat>().As<MediaFormat>().InstancePerDependency();
		}
	}
}