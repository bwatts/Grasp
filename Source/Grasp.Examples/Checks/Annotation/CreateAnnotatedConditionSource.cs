using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using FakeItEasy;
using Grasp.Checks.Conditions;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Annotation
{
	public class CreateAnnotatedConditionSource : Behavior
	{
		Type _targetType;
		AnnotatedConditionSource _source;

		protected override void Given()
		{
			_targetType = typeof(TestTarget);
		}

		protected override void When()
		{
			_source = new AnnotatedConditionSource(_targetType);
		}

		[Then]
		public void HasOriginalTargetType()
		{
			Assert.That(_source.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void HasDefaultConditionName()
		{
			Assert.That(_source.DefaultConditionName, Is.EqualTo(ConditionKey.DefaultName));
		}

		private class TestTarget
		{}
	}
}