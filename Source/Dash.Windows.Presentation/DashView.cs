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
		private DateTime? _whenModified;

		public DashView(string username, IUserDashRepository dashRepository)
		{
			Contract.Requires(username != null);
			Contract.Requires(dashRepository != null);

			Username = username;
			_dashRepository = dashRepository;

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

		private void GetTopicsAsync()
		{
			GetTopicsOperation.ExecuteAsync(() => _dashRepository.GetDash(Username), OnGetDashCompleted);
		}

		private void OnGetDashCompleted(UserDash dash)
		{
			Topics.Clear();

			foreach(var topic in dash.Topics)
			{
				Topics.Add(new TopicView(topic));
			}

			var change = NotionLifetime.GetWhenModified(dash);

			WhenModified = change == null ? (DateTime?) null : change.When;
		}
	}
}