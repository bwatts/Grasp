using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Calculators
{
	public class CreateCompositeCalculator : Behavior
	{
		IEnumerable<ICalculator> _calculators;
		CompositeCalculator _calculator;

		protected override void Given()
		{
			_calculators = new[] { A.Fake<ICalculator>(), A.Fake<ICalculator>() };
		}

		protected override void When()
		{
			_calculator = new CompositeCalculator(_calculators);
		}

		[Then]
		public void HasCalculationsInOrder()
		{
			Assert.That(_calculator.Calculators.SequenceEqual(_calculators), Is.True);
		}
	}
}