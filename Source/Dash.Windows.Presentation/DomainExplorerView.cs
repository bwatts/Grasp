using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak;
using Cloak.Wpf.Mvvm;
using Grasp.Semantics;
using Grasp.Semantics.Discovery;

namespace Dash.Windows.Presentation
{
	public class DomainExplorerView : ViewModel
	{
		public DomainExplorerView(IDomainModelSource domainModelSource)
		{
			Contract.Requires(domainModelSource != null);

			DomainModels = domainModelSource
				.GetDomainModelBindings()
				.Select(domainModelBinding => domainModelBinding.GetDomainModel())
				.OrderBy(domainModel => domainModel.Name)
				.ToReadOnlyObservableCollection();
		}

		public ReadOnlyObservableCollection<DomainModel> DomainModels { get; private set; }
	}
}