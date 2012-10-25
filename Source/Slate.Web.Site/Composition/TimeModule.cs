using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Cloak.NaturalText;
using Cloak.Time;

namespace Slate.Web.Site.Composition
{
	public class TimeModule : BuilderModule
	{
		public TimeModule()
		{
			Register(c => new NaturalTextTimeContext(new PlatformTimeContext())).As<ITimeContext>().SingleInstance();
		}
	}
}