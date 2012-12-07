using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Forms
{
	public class ParseResult<T> : Notion
	{
		public static readonly Field<bool> SuccessField = Field.On<ParseResult<T>>.For(x => x.Success);
		public static readonly Field<T> ValueField = Field.On<ParseResult<T>>.For(x => x.Value);

		public ParseResult()
		{}

		public ParseResult(T value)
		{
			Value = value;

			Success = true;
		}

		public bool Success { get { return GetValue(SuccessField); } private set { SetValue(SuccessField, value); } }
		public T Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }
	}
}