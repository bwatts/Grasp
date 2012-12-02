using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Grasp.Checks.Rules;
using Xunit;

namespace Grasp.Knowledge.Definition
{
	public class ValidationRules
	{
		[Fact] public void Create()
		{
			var rootNamespace = Namespace.Root;
			var outputVariableIdentifier = new Identifier("SomeRule");
			var expression = Expression.Constant(true);

			var validationRule = new ValidationRule(rootNamespace, outputVariableIdentifier, expression);

			validationRule.RootNamespace.Should<Namespace>().Be(rootNamespace);
			validationRule.OutputVariableIdentifier.Should().Be(outputVariableIdentifier);
			validationRule.Expression.Should().Be(expression);
		}

		[Fact] public void GetCalculation()
		{
			var rootNamespace = new Namespace("SomeNamespace");
			var outputVariableIdentifier = new Identifier("SomeRule");
			var expression = Expression.Constant(true);
			var validationRule = new ValidationRule(rootNamespace, outputVariableIdentifier, expression);

			var calculation = validationRule.GetCalculation();

			calculation.ShouldCalculate("SomeNamespace.__validation.SomeRule", typeof(bool));
			calculation.Expression.Should().Be(expression);
		}
	}
}