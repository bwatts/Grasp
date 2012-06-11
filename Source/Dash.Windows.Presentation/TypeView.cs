using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Knowledge.Work;
using Grasp.Semantics;

namespace Dash.Windows.Presentation
{
	public abstract class TypeView : ViewModel, ITopicSource
	{
		private readonly TypeModel _type;
		private readonly IDashContext _dashContext;
		private readonly Func<TypeModel, TypeExplorerView> _typeExplorerFactory;

		protected TypeView(TypeModel type, IDashContext dashContext, Func<TypeModel, TypeExplorerView> typeExplorerFactory)
		{
			Contract.Requires(type != null);
			Contract.Requires(dashContext != null);
			Contract.Requires(typeExplorerFactory != null);

			_type = type;
			_dashContext = dashContext;
			_typeExplorerFactory = typeExplorerFactory;

			ExploreCommand = new MethodCommand(OnExplore);
		}

		public string Name
		{
			get { return _type.Type.Name; }
		}

		public ICommand ExploreCommand { get; private set; }

		private void OnExplore()
		{
			var explorer = _typeExplorerFactory(_type);

			var topic = new Topic(Name, TopicStatus.Default, explorer);

			NotionLifetime.SetWhenModified(topic, Change.EntityModified(topic, DateTime.Now));

			_dashContext.AddTopic(this, topic);
		}
	}
}