using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis
{
	public class CalculationToString : Behavior
	{
		Calculation _calculation;
		string _text;

		protected override void Given()
		{
			_calculation = new Calculation(new Variable("Grasp", "Test", typeof(int)), Expression.Constant(0));
		}

		protected override void When()
		{
			_text = _calculation.ToString();
		}

		[Then]
		public void IsEquation()
		{
			Assert.That(_text, Is.EqualTo(String.Format("{0} = {1}", _calculation.OutputVariable, _calculation.Expression)));
		}
	}
}