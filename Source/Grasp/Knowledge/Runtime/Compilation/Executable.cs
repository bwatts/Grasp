﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	/// <summary>
	/// An executable version of a <see cref="_schema"/> which applies its calculations
	/// </summary>
	public class Executable : Notion
	{
		public static readonly Field<Schema> _schemaField = Field.On<Executable>.For(x => x._schema);
		public static readonly Field<ICalculator> _calculatorField = Field.On<Executable>.For(x => x._calculator);

		private Schema _schema { get { return GetValue(_schemaField); } set { SetValue(_schemaField, value); } }
		private ICalculator _calculator { get { return GetValue(_calculatorField); } set { SetValue(_calculatorField, value); } }

		/// <summary>
		/// Initializes an executable with the specified schema and calculator
		/// </summary>
		/// <param name="schema">The schema which generated this executable</param>
		/// <param name="calculator">The calculator which applies the schema's calculations to bound schemas generated by this executable</param>
		public Executable(Schema schema, ICalculator calculator)
		{
			Contract.Requires(schema != null);
			Contract.Requires(calculator != null);

			_schema = schema;
			_calculator = calculator;
		}

		/// <summary>
		/// Binds this executable's schema using the specified initial state
		/// </summary>
		/// <param name="initialState">The initial state of the schema's variables</param>
		/// <returns>A schema binding with the specified initial state</returns>
		public SchemaBinding Bind(ISnapshot initialState)
		{
			Contract.Requires(initialState != null);

			return new SchemaBinding(_schema, _calculator, GetBindings(initialState));
		}

		private IEnumerable<VariableBinding> GetBindings(ISnapshot initialState)
		{
			return _schema.Variables.Select(variable => new VariableBinding(variable.Name, initialState.GetValue(variable.Name)));
		}
	}
}