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
		private readonly IDomainModelSource _domainModelSource;

		public DomainExplorerView(IDomainModelSource domainModelSource)
		{
			Contract.Requires(domainModelSource != null);

			_domainModelSource = domainModelSource;

			DomainModels = new ObservableCollection<DomainModel>();

			GetDomainModelsOperation = new OperationModel();

			GetDomainModelsAsync();
		}

		public ObservableCollection<DomainModel> DomainModels { get; private set; }

		public OperationModel GetDomainModelsOperation { get; private set; }

		private void GetDomainModelsAsync()
		{
			GetDomainModelsOperation.ExecuteAsync(() => _domainModelSource.GetDomainModelBindings(), OnGetDomainModelBindingsComplete);
		}

		private void OnGetDomainModelBindingsComplete(IEnumerable<DomainModelBinding> domainModelBindings)
		{
			DomainModels.Clear();

			foreach(var domainModelBinding in domainModelBindings)
			{
				DomainModels.Add(domainModelBinding.GetDomainModel());
			}
		}
	}
}