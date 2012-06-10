using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Dash.Infrastructure;
using Dash.Windows.Presentation;
using Grasp.Knowledge;
using Grasp.Knowledge.Work;

namespace Dash.Windows.Composition
{
	public class DashModule : BuilderModule
	{
		public DashModule()
		{
			// TODO: Real dash repository
			RegisterType<FakeDashRepository>().As<IUserDashRepository>().SingleInstance();

			Register(c => new DashView("bwatts", c.Resolve<IUserDashRepository>())).InstancePerDependency();
		}



		private sealed class FakeDashRepository : IUserDashRepository
		{
			private readonly Func<DomainExplorerView> _domainExplorerViewFactory;

			public FakeDashRepository(Func<DomainExplorerView> domainExplorerViewFactory)
			{
				_domainExplorerViewFactory = domainExplorerViewFactory;
			}

			public UserDash GetDash(string username)
			{
				var dash = new UserDash();

				var topic = new Topic("Domain Explorer", TopicStatus.Default, _domainExplorerViewFactory());

				dash.SetValue(UserDash.UsernameField, username);
				dash.SetValue(UserDash.TopicsField, new Many<Topic>(topic));

				NotionLifetime.SetWhenModified(topic, Change.EntityModified(topic, DateTime.Now));
				NotionLifetime.SetWhenModified(dash, Change.EntityCreated(dash, DateTime.Now));

				return dash;
			}
		}






	}
}