using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Dash.Infrastructure;

namespace Dash.Windows.Presentation
{
	public sealed class DashViewContext : IDashContext
	{
		private readonly Func<DashView> _dashViewAccessor;

		public DashViewContext(Func<DashView> dashViewAccessor)
		{
			Contract.Requires(dashViewAccessor != null);

			_dashViewAccessor = dashViewAccessor;
		}

		public void AddTopic(ITopicSource source, Topic topic)
		{
			var dashView = _dashViewAccessor();

			dashView.AddTopic(topic);
		}

		public void RemoveTopic(Topic topic)
		{
			var dashView = _dashViewAccessor();

			dashView.RemoveTopic(topic);
		}
	}
}