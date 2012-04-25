﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Characters
{
	public class GetIsSeparatorRuleNonChar : Behavior
	{
		IsSeparatorMethod _method;
		GraspException _exception;

		protected override void Given()
		{
			_method = new IsSeparatorMethod();
		}

		protected override void When()
		{
			try
			{
				_method.GetRule(typeof(string));
			}
			catch(GraspException ex)
			{
				_exception = ex;
			}
		}

		[Then]
		public void Throws()
		{
			Assert.That(_exception, Is.Not.Null);
		}
	}
}