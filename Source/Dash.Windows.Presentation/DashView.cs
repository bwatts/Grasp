using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Knowledge.Work;

namespace Dash.Windows.Presentation
{
	public class DashView : ViewModel
	{
		private readonly IUserDashRepository _dashRepository;
		private readonly Func<Topic, TopicView> _topicFactory;
		private DateTime? _whenModified;

		public DashView(string username, IUserDashRepository dashRepository, Func<Topic, TopicView> topicFactory)
		{
			Contract.Requires(username != null);
			Contract.Requires(dashRepository != null);
			Contract.Requires(topicFactory != null);

			Username = username;
			_dashRepository = dashRepository;
			_topicFactory = topicFactory;

			Topics = new ObservableCollection<TopicView>();

			GetTopicsOperation = new OperationModel();

			GetTopicsAsync();
		}

		public string Username { get; private set; }

		public DateTime? WhenModified
		{
			get { return _whenModified; }
			private set { SetProperty("WhenModified", ref _whenModified, value); }
		}

		public ObservableCollection<TopicView> Topics { get; private set; }

		public OperationModel GetTopicsOperation { get; private set; }

		public void AddTopic(Topic topic)
		{
			Contract.Requires(topic != null);

			Topics.Add(_topicFactory(topic));
		}

		public void RemoveTopic(Topic topic)
		{
			Contract.Requires(topic != null);

			var removedTopics = Topics.Where(dashTopic => dashTopic.Topic == topic).ToList();

			foreach(var removedTopic in removedTopics)
			{
				Topics.Remove(removedTopic);
			}
		}

		private void GetTopicsAsync()
		{
			GetTopicsOperation.ExecuteAsync(() => _dashRepository.GetDash(Username), OnGetDashCompleted);
		}

		private void OnGetDashCompleted(UserDash dash)
		{
			Topics.Clear();

			foreach(var topic in dash.Topics)
			{
				AddTopic(topic);
			}

			var change = NotionLifetime.GetWhenModified(dash);

			WhenModified = change == null ? (DateTime?) null : change.When;
		}
	}
}