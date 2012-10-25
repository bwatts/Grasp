using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp
{
	/// <summary>
	/// A binding of a field to a value
	/// </summary>
	public class FieldBinding
	{
		internal FieldBinding(Field field, object value)
		{
			Field = field;
			Value = value;
		}

		public Field Field { get; private set; }

		public object Value { get; private set; }

		public override string ToString()
		{
			return "{0} = {1}".FormatCurrent(Field, Value);
		}
	}

	/// <summary>
	/// A strongly-typed binding of a field to a value
	/// </summary>
	public sealed class FieldBinding<TValue> : FieldBinding
	{
		internal FieldBinding(Field<TValue> field, TValue value) : base(field, value)
		{
			Field = field;
			Value = value;
		}

		public new Field<TValue> Field { get; private set; }

		public new TValue Value { get; private set; }
	}
}