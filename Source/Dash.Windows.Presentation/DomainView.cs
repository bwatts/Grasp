using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Semantics;

namespace Dash.Windows.Presentation
{
	public class DomainView : ViewModel
	{
		public DomainView(DomainModel domain, Func<NamespaceModel, NamespaceView> namespaceFactory)
		{
			Contract.Requires(domain != null);

			Namespaces = domain.Namespaces.Select(namespaceFactory).ToList().AsReadOnly();
		}

		public ReadOnlyCollection<NamespaceView> Namespaces { get; private set; }
	}
}