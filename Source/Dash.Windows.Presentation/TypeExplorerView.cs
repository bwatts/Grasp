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
	public class TypeExplorerView : ViewModel
	{
		private readonly EntityModel _type;

		public TypeExplorerView(TypeModel type, Func<Field, FieldView> fieldFactory)
		{
			Contract.Requires(type != null);

			// TODO: Remove cast

			_type = (EntityModel) type;

			Fields = _type.Fields.Select(fieldFactory).ToReadOnlyObservableCollection();
		}

		public string Name
		{
			get { return _type.Type.Name; }
		}

		public ReadOnlyObservableCollection<FieldView> Fields { get; private set; }
	}
}