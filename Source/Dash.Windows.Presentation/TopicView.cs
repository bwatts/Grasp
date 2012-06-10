using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;
using Cloak.Wpf.Mvvm;
using Grasp.Knowledge.Work;
using Grasp.Semantics;
using Grasp.Semantics.Discovery;

namespace Dash.Windows.Presentation
{
	public class TopicView : ViewModel
	{
		private readonly Topic _topic;

		public TopicView(Topic topic)
		{
			Contract.Requires(topic != null);

			_topic = topic;
		}

		public string Title
		{
			get { return _topic.Title; }
		}

		public TopicStatus Status
		{
			get { return _topic.Status; }
		}

		public object Content
		{
			get { return _topic.Content; }
		}

		public DateTime? WhenModified
		{
			get
			{
				var change = NotionLifetime.GetWhenModified(_topic);

				return change == null ? (DateTime?) null : change.When;
			}
		}
	}
}