using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Grasp.Checks.Rules;
using Xunit;

namespace Grasp.Knowledge.Structure
{
	public class Validators
	{
		[Fact] public void Create()
		{
			var outputVariableIdentifier = new Identifier("A");
			var rule = Rule.True;

			var validator = new Validator(outputVariableIdentifier, rule);

			validator.OutputVariableIdentifier.Should().Be(outputVariableIdentifier);
			validator.Rule.Should().Be(rule);
		}

		[Fact] public void GetCalculation()
		{
			var variable = new Variable<int>("SomeValue");
			var outputVariableIdentifier = new Identifier("SomeRule");
			var rule = Rule.True;
			var validator = new Validator(outputVariableIdentifier, rule);

			var calculation = validator.GetCalculation(variable);

			calculation.OutputVariable.Name.Namespace.Should<Namespace>().Be(variable.Name.ToNamespace());
			calculation.OutputVariable.Name.Identifier.Should<Identifier>().Be(outputVariableIdentifier);
			calculation.Expression.Should().BeAssignableTo<InvocationExpression>();
		}
	}
}