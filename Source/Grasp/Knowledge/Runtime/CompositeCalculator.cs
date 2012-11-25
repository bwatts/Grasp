using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime
{
	/// <summary>
	/// A series of calculators which applies to bound schemas
	/// </summary>
	public sealed class CompositeCalculator : Notion, ICalculator
	{
		public static readonly Field<ManyInOrder<ICalculator>> _calculatorsField = Field.On<CompositeCalculator>.For(x => x._calculators);

		private ManyInOrder<ICalculator> _calculators { get { return GetValue(_calculatorsField); } set { SetValue(_calculatorsField, value); } }

		/// <summary>
		/// Initializes a composite calculator with the specified calculators
		/// </summary>
		/// <param name="calculators">The series of applied calculators</param>
		public CompositeCalculator(IEnumerable<ICalculator> calculators)
		{
			Contract.Requires(calculators != null);

			_calculators = calculators.ToManyInOrder();
		}

		/// <summary>
		/// Initializes a composite calculator with the specified calculators
		/// </summary>
		/// <param name="calculators">The series of applied calculators</param>
		public CompositeCalculator(params ICalculator[] calculators) : this(calculators as IEnumerable<ICalculator>)
		{}

		/// <summary>
		/// Applies the series of calculators to the specified bound schema
		/// </summary>
		/// <param name="schemaBinding">A bound schema to which to apply the series of calculators</param>
		public void ApplyCalculation(SchemaBinding schemaBinding)
		{
			foreach(var calculator in _calculators)
			{
				calculator.ApplyCalculation(schemaBinding);
			}
		}
	}
}