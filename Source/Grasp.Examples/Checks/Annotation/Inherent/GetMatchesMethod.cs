using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Methods;
using NUnit.Framework;

namespace Grasp.Checks.Annotation.Inherent
{
	public class GetMatchesMethod : Behavior
	{
		CheckMatchesAttribute _attribute;
		ICheckMethod _method;

		protected override void Given()
		{
			_attribute = new CheckMatchesAttribute("");
		}

		protected override void When()
		{
			_method = _attribute.GetCheckMethod();
		}

		[Then]
		public void IsMatchesMethod()
		{
			Assert.That(_method, Is.InstanceOf<MatchesMethod>());
		}
	}
}