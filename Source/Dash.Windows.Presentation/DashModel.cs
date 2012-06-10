using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Cloak.Wpf.Mvvm;

namespace Dash.Windows.Presentation
{
	public class DashModel : ViewModel
	{
		public DashModel()
		{
			Topics = new ObservableCollection<Topic>
			{
				new Topic("Proof of concept", TopicStatus.Default, "-[content]-"),
				new Topic("Part 2", new TopicStatus("Test"), "-[content 2]-")
			};
		}

		public ObservableCollection<Topic> Topics { get; private set; }
	}
}