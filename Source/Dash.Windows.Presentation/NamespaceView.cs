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
	public class NamespaceView : ViewModel, ITopicSource
	{
		private readonly NamespaceModel _namespace;
		private readonly IDashContext _dashContext;
		private readonly Func<NamespaceModel, NamespaceExplorerView> _namespaceExplorerFactory;

		public NamespaceView(NamespaceModel @namespace, IDashContext dashContext, Func<NamespaceModel, NamespaceExplorerView> namespaceExplorerFactory)
		{
			Contract.Requires(@namespace != null);
			Contract.Requires(dashContext != null);
			Contract.Requires(namespaceExplorerFactory != null);

			_namespace = @namespace;
			_dashContext = dashContext;
			_namespaceExplorerFactory = namespaceExplorerFactory;

			ExploreCommand = new MethodCommand(OnExplore);
		}

		public string Name
		{
			get { return _namespace.Name; }
		}

		public int TypeCount
		{
			get { return _namespace.Types.Count(); }
		}

		public ICommand ExploreCommand { get; private set; }

		private void OnExplore()
		{
			var explorer = _namespaceExplorerFactory(_namespace);

			var topic = new Topic(_namespace.Name, TopicStatus.Default, explorer);

			NotionLifetime.SetWhenModified(topic, Change.EntityModified(topic, DateTime.Now));

			_dashContext.AddTopic(this, topic);
		}
	}
}