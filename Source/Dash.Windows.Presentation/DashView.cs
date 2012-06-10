using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Cloak.Wpf.Mvvm;

namespace Dash.Windows.Presentation
{
	public class DashView : ViewModel
	{
		public DashView()
		{
			Topics = new ObservableCollection<Topic>();
		}

		public ObservableCollection<Topic> Topics { get; private set; }
	}
}