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
	public class GetIsEqualToMethod : Behavior
	{
		int _value;
		CheckIsEqualToAttribute _attribute;
		ICheckMethod _method;

		protected override void Given()
		{
			_value = 1;

			_attribute = new CheckIsEqualToAttribute(_value);
		}

		protected override void When()
		{
			_method = _attribute.GetCheckMethod();
		}

		[Then]
		public void IsIsEqualToMethod()
		{
			Assert.That(_method, Is.InstanceOf<IsEqualToMethod>());
		}

		[Then]
		public void HasOriginalValue()
		{
			var isEqualToMethod = (IsEqualToMethod) _method;

			Assert.That(isEqualToMethod.Value, Is.EqualTo(_value));
		}
	}
}