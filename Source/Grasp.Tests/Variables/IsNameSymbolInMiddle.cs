using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables
{
	public class IsNameSymbolInMiddle : Behavior
	{
		bool _isName;

		protected override void Given()
		{}

		protected override void When()
		{
			_isName = Variable.IsName("Te$t");
		}

		[Then]
		public void IsFalse()
		{
			Assert.That(_isName, Is.False);
		}
	}
}