using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Semantics;

namespace Dash.Windows.Presentation
{
	public class NamespaceView : ViewModel, ITopicSource
	{
		private readonly NamespaceModel _namespace;
		private readonly IDashContext _dashContext;

		public NamespaceView(NamespaceModel @namespace, IDashContext dashContext)
		{
			Contract.Requires(@namespace != null);
			Contract.Requires(dashContext != null);

			_namespace = @namespace;
			_dashContext = dashContext;

			OpenCommand = new MethodCommand(OnOpen);
		}

		public string Name
		{
			get { return _namespace.Name; }
		}

		public int TypeCount
		{
			get { return _namespace.Types.Count(); }
		}

		public ICommand OpenCommand { get; private set; }

		private void OnOpen()
		{
			_dashContext.AddTopic(this, new Topic(_namespace.Name, TopicStatus.Default, _namespace));
		}
	}
}