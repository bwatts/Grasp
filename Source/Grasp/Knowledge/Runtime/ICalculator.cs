using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Runtime
{
	/// <summary>
	/// Describes a calculation which applies to bound schemas
	/// </summary>
	[ContractClass(typeof(ICalculatorContract))]
	public interface ICalculator
	{
		/// <summary>
		/// Applies the encapsulated calculation to the specified bound schema
		/// </summary>
		/// <param name="schemaBinding">The schema binding to which to apply the encapsulated calculation</param>
		void ApplyCalculation(SchemaBinding schemaBinding);
	}

	[ContractClassFor(typeof(ICalculator))]
	internal abstract class ICalculatorContract : ICalculator
	{
		void ICalculator.ApplyCalculation(SchemaBinding schemaBinding)
		{
			Contract.Requires(schemaBinding != null);
		}
	}
}