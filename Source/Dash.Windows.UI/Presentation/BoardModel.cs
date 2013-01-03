using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Wpf.Mvvm;

namespace Dash.Windows.UI.Presentation
{
	public class BoardModel : ViewModel
	{
		public BoardModel(IEnumerable<TopicModel> topics = null)
		{
			Topics = (topics ?? Enumerable.Empty<TopicModel>()).ToReadOnlyObservableCollection();
		}

		public ReadOnlyObservableCollection<TopicModel> Topics { get; private set; }
	}
}