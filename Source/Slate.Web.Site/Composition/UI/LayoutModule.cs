using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Slate.Web.Site.Presentation;

namespace Slate.Web.Site.Composition.UI
{
	public class LayoutModule : BuilderModule
	{
		public LayoutModule()
		{
			RegisterType<LayoutModelFactory>().As<ILayoutModelFactory>().SingleInstance();
		}
	}
}