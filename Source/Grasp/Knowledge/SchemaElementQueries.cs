using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge
{
	public static class SchemaElementQueries
	{
		public static Variable GetVariable(this IEnumerable<Variable> variables, FullName name)
		{
			Contract.Requires(variables != null);
			Contract.Requires(name != null);

			return variables.FirstOrDefault(variable => variable.Name == name);
		}

		public static Calculation GetCalculation(this IEnumerable<Calculation> calculations, FullName outputVariableName)
		{
			Contract.Requires(calculations != null);
			Contract.Requires(outputVariableName != null);

			var x = calculations.First();

			return calculations.FirstOrDefault(calculation => calculation.OutputVariable.Name == outputVariableName);
		}

		public static Variable GetOutputVariable(this IEnumerable<Calculation> calculations, FullName name)
		{
			Contract.Requires(calculations != null);
			Contract.Requires(name != null);

			var calculation = calculations.GetCalculation(name);

			return calculation == null ? null : calculation.OutputVariable;
		}

		public static Variable GetVariable(this Schema schema, FullName name)
		{
			Contract.Requires(schema != null);
			Contract.Requires(name != null);

			return schema.EffectiveVariables.GetVariable(name);
		}

		public static Calculation GetCalculation(this Schema schema, FullName outputVariableName)
		{
			Contract.Requires(schema != null);
			Contract.Requires(outputVariableName != null);

			return schema.Calculations.GetCalculation(outputVariableName);
		}

		public static Variable GetOutputVariable(this Schema schema, FullName name)
		{
			Contract.Requires(schema != null);
			Contract.Requires(name != null);

			return schema.Calculations.GetOutputVariable(name);
		}
	}
}