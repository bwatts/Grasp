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
			RegisterType<FakeDashRepository>().As<IBoardRepository>().SingleInstance();



			RegisterType<TopicView>().InstancePerDependency();

			Register(c => new DashView("bwatts", c.Resolve<IBoardRepository>(), c.Resolve<Func<Topic, TopicView>>())).InstancePerLifetimeScope();

			RegisterType<DashViewContext>().As<IBoardContext>().InstancePerDependency();
		}



		private sealed class FakeDashRepository : IBoardRepository
		{
			private readonly Func<DomainExplorerView> _domainExplorerViewFactory;

			public FakeDashRepository(Func<DomainExplorerView> domainExplorerViewFactory)
			{
				_domainExplorerViewFactory = domainExplorerViewFactory;
			}

			public Board GetBoard(string username)
			{
				var dash = new Board();

				var topic = new Topic("Domain Explorer", ProcessStatus.Over, _domainExplorerViewFactory());

				dash.SetValue(Board.UsernameField, username);
				dash.SetValue(Board.TopicsField, new Many<Topic>(topic));

				NotionLifetime.SetWhenModified(topic, Change.EntityModified(topic, DateTime.Now));
				NotionLifetime.SetWhenModified(dash, Change.EntityCreated(dash, DateTime.Now));

				return dash;
			}
		}






	}
}