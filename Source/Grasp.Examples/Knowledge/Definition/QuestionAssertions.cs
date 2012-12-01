using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Grasp.Knowledge.Definition
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class QuestionAssertions
	{
		public static void ShouldHaveVariables(this Schema schema, params string[] names)
		{
			Contract.Requires(schema != null);
			Contract.Requires(names != null);

			var schemaNames = schema.Variables.Select(variable => variable.Name.Value).ToList();

			schemaNames.Should().HaveCount(names.Length, "Schema did not have the expected number of variables");

			schemaNames.Should().Contain(names, reason: "Schema did not contain the exact set of expected variables");
		}

		public static void ShouldCalculate(this Schema schema, params string[] outputVariableNames)
		{
			Contract.Requires(schema != null);
			Contract.Requires(outputVariableNames != null);

			var schemaOutputVariableNames = schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).ToList();

			schemaOutputVariableNames.Should().HaveCount(outputVariableNames.Length);

			schemaOutputVariableNames.Should().HaveCount(outputVariableNames.Length, "Schema did not have the expected number of output variables");

			schemaOutputVariableNames.Should().Contain(outputVariableNames, reason: "Schema did not contain the exact set of expected output variables");
		}

		public static void ShouldHaveName(this Variable variable, string name)
		{
			Contract.Requires(variable != null);
			Contract.Requires(name != null);

			variable.Name.Value.Should().Be(name, "Variable did not have the expected name");
		}

		public static void ShouldCalculate(this Calculation calculation, string name, Type type)
		{
			Contract.Requires(calculation != null);
			Contract.Requires(name != null);
			Contract.Requires(type != null);

			calculation.OutputVariable.Name.Value.Should().Be(name, "Calculation did not have the expected output variable");

			calculation.OutputVariable.Type.Should().Be(type, "Output variable was not of the expected type");
		}
	}
}