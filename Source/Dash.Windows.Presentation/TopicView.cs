using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cloak;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Knowledge.Work;
using Grasp.Semantics;
using Grasp.Semantics.Discovery;

namespace Dash.Windows.Presentation
{
	public class TopicView : ViewModel
	{
		private readonly IBoardContext _dashContext;

		public TopicView(Topic topic, IBoardContext dashContext)
		{
			Contract.Requires(topic != null);
			Contract.Requires(dashContext != null);

			Topic = topic;
			_dashContext = dashContext;

			CloseCommand = new MethodCommand(OnClose);
		}

		internal Topic Topic { get; private set; }

		public string Title
		{
			get { return Topic.Title; }
		}

		public TopicStatus Status
		{
			get { return Topic.Status; }
		}

		public object Content
		{
			get { return Topic.Content; }
		}

		public DateTime? WhenModified
		{
			get
			{
				var change = NotionLifetime.GetWhenModified(Topic);

				return change == null ? (DateTime?) null : change.When;
			}
		}

		public ICommand CloseCommand { get; private set; }

		private void OnClose()
		{
			_dashContext.RemoveTopic(Topic);
		}
	}
}