using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Dash.Windows.Presentation;

namespace Dash.Windows.Composition
{
	public class DashModule : BuilderModule
	{
		public DashModule()
		{
			Register(c =>
			{
				var dashView = new DashView();

				dashView.Topics.Add(new Topic("Domain Explorer", TopicStatus.Default, c.Resolve<DomainExplorerView>()));

				return dashView;
			})
			.InstancePerDependency();
		}
	}
}