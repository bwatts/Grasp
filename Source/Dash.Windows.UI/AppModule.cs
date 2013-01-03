using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Cloak;
using Cloak.Autofac;
using Cloak.Wpf.Mvvm;
using Dash.Windows.UI.Presentation;

namespace Dash.Windows.UI
{
	public class AppModule : BuilderModule
	{
		public AppModule()
		{
			//RegisterType<BoardModel>().SingleInstance();
			Register(c => new BoardModel(Params.Of(new TopicModel(), new TopicModel(), new TopicModel()))).SingleInstance();

			RegisterType<Shell>().SingleInstance();
		}
	}
}