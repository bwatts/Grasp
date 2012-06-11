using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak.Wpf.Mvvm;
using Grasp.Knowledge;

namespace Dash.Windows.Presentation
{
	public abstract class FieldView : ViewModel
	{
		public static FieldView SingularOrPlural(Field field)
		{
			Contract.Requires(field != null);

			return field.IsMany ? new PluralFieldView(field) : new SingularFieldView(field) as FieldView;
		}

		protected FieldView(Field field)
		{
			Contract.Requires(field != null);

			Field = field;
		}

		protected Field Field { get; private set; }

		public string Name
		{
			get { return Field.Name; }
		}
	}
}