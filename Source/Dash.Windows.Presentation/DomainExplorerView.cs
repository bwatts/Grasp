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
		public DomainExplorerView(IDomainModelSource domainModelSource, Func<DomainModel, DomainView> domainFactory)
		{
			Contract.Requires(domainModelSource != null);

			Domains = domainModelSource
				.GetDomainModelBindings()
				.Select(domainModelBinding => domainModelBinding.GetDomainModel())
				.OrderBy(domainModel => domainModel.Name)
				.Select(domainFactory)
				.ToReadOnlyObservableCollection();
		}

		public ReadOnlyObservableCollection<DomainView> Domains { get; private set; }
	}
}