﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables.Names
{
	public class SpaceFirstIsName : Behavior
	{
		bool _isName;

		protected override void Given()
		{}

		protected override void When()
		{
			_isName = Variable.IsName(" Test");
		}

		[Then]
		public void IsFalse()
		{
			Assert.That(_isName, Is.False);
		}
	}
}