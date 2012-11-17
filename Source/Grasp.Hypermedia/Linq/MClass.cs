using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MClass : ComparableValue<MClass, string>, IStackable<MClass>
	{
		public static readonly MClass Empty = "";

		public static implicit operator MClass(string value)
		{
			return new MClass(value);
		}

		public static implicit operator string(MClass @class)
		{
			return @class.ToString();
		}

		public MClass(string value) : base(value)
		{}
	}
}