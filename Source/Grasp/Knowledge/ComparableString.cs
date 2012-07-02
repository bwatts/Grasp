using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	public class ComparableString : ComparableValue<ComparableString, string>
	{
		public static readonly ComparableString Empty = new ComparableString("");

		protected ComparableString(string value) : base(value)
		{}
	}
}