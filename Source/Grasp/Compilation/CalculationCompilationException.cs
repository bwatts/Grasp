using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Compilation
{
	public class CalculationCompilationException : Exception
	{
		public CalculationCompilationException(Calculation calculation, Expression lambdaBody)
		{
			Contract.Requires(calculation != null);
			Contract.Requires(lambdaBody != null);

			Calculation = calculation;
			LambdaBody = lambdaBody;
		}

		public CalculationCompilationException(Calculation calculation, Expression lambdaBody, string message)
			: base(message)
		{
			Contract.Requires(calculation != null);
			Contract.Requires(lambdaBody != null);

			Calculation = calculation;
			LambdaBody = lambdaBody;
		}

		public CalculationCompilationException(Calculation calculation, Expression lambdaBody, string message, Exception inner)
			: base(message, inner)
		{
			Contract.Requires(calculation != null);
			Contract.Requires(lambdaBody != null);

			Calculation = calculation;
			LambdaBody = lambdaBody;
		}

		public Calculation Calculation { get; private set; }

		public Expression LambdaBody { get; private set; }
	}
}