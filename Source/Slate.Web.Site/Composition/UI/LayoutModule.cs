using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Slate.Web.Presentation;

namespace Slate.Web.Site.Composition.UI
{
	public class LayoutModule : BuilderModule
	{
		public LayoutModule()
		{
			RegisterType<LayoutFactory>().As<ILayoutFactory>().SingleInstance();
		}
	}
}