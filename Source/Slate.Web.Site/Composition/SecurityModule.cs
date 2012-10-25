using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Slate.Web.Presentation;

namespace Slate.Web.Site.Composition
{
	public class SecurityModule : BuilderModule
	{
		public SecurityModule()
		{
			RegisterType<FakeUserContext>().As<IUserContext>().SingleInstance();
		}



		private sealed class FakeUserContext : IUserContext
		{
			public UserIdentity GetIdentity()
			{
				return new UserIdentity("bwatts", new HtmlLink("settings/users/bwatts", "bwatts", "User details: bwatts"));
			}
		}




	}
}