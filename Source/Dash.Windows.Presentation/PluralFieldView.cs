using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash.Windows.Presentation
{
	public class PluralFieldView : FieldView
	{
		public PluralFieldView(Field field) : base(field)
		{
			Contract.Requires(field.IsMany);
		}

		public Type ElementType
		{
			get { return Field.GetManyElementType(); }
		}
	}
}