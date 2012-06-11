using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash.Windows.Presentation
{
	public class SingularFieldView : FieldView
	{
		public SingularFieldView(Field field) : base(field)
		{
			Contract.Requires(!field.IsMany);
		}

		public Type ValueType
		{
			get { return Field.ValueType; }
		}
	}
}