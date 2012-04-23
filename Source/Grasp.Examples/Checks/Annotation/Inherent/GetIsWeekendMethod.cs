﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Methods;
using NUnit.Framework;

namespace Grasp.Checks.Annotation.Inherent
{
	public class GetIsWeekendMethod : Behavior
	{
		CheckIsWeekendAttribute _attribute;
		ICheckMethod _method;

		protected override void Given()
		{
			_attribute = new CheckIsWeekendAttribute();
		}

		protected override void When()
		{
			_method = _attribute.GetCheckMethod();
		}

		[Then]
		public void IsIsNullMethod()
		{
			Assert.That(_method, Is.InstanceOf<IsWeekendMethod>());
		}
	}
}