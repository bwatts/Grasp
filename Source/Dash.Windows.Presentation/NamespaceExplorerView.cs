using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cloak;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Knowledge;
using Grasp.Semantics;

namespace Dash.Windows.Presentation
{
	public class NamespaceExplorerView : ViewModel
	{
		private readonly NamespaceModel _namespace;

		public NamespaceExplorerView(NamespaceModel @namespace, Func<TypeModel, TypeView> typeFactory)
		{
			Contract.Requires(@namespace != null);
			Contract.Requires(typeFactory != null);

			_namespace = @namespace;

			Types = _namespace.Types.Select(typeFactory).ToReadOnlyObservableCollection();
		}

		public string Name
		{
			get { return _namespace.Name; }
		}

		public ReadOnlyObservableCollection<TypeView> Types { get; private set; }
	}
}