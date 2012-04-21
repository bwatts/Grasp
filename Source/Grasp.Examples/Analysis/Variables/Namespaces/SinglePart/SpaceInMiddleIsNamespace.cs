using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis.Variables.Namespaces.SinglePart
{
	public class SpaceInMiddleIsNamespace : Behavior
	{
		bool _isNamespace;

		protected override void Given()
		{}

		protected override void When()
		{
			_isNamespace = Variable.IsNamespace("Te st");
		}

		[Then]
		public void IsFalse()
		{
			Assert.That(_isNamespace, Is.False);
		}
	}
}