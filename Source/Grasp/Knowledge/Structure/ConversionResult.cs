using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Structure
{
	public sealed class ConversionResult<T> : Notion
	{
		public static readonly Field<bool> SuccessField = Field.On<ConversionResult<T>>.For(x => x.Success);
		public static readonly Field<T> ValueField = Field.On<ConversionResult<T>>.For(x => x.Value);

		public ConversionResult()
		{}

		public ConversionResult(T value)
		{
			Success = true;

			Value = value;
		}

		public bool Success { get { return GetValue(SuccessField); } private set { SetValue(SuccessField, value); } }
		public T Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public Type ValueType
		{
			get { return typeof(T); }
		}

		public override string ToString()
		{
			return Success ? Resources.ConvertResultSuccess.FormatCurrent(Value) : Resources.ConvertResultFailure;
		}
	}
}