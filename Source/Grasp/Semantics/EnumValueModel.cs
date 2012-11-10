using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class EnumValueModel : Notion
	{
		public static readonly Field<string> NameField = Field.On<EnumValueModel>.For(x => x.Name);
		public static readonly Field<int> NumericValueField = Field.On<EnumValueModel>.For(x => x.NumericValue);
		public static readonly Field<object> ValueField = Field.On<EnumValueModel>.For(x => x.Value);

		public EnumValueModel(string name, int numericValue, object value)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));
			Contract.Requires(value != null);

			Name = name;
			NumericValue = numericValue;
			Value = value;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public int NumericValue { get { return GetValue(NumericValueField); } private set { SetValue(NumericValueField, value); } }
		public object Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public override string ToString()
		{
			return Name;
		}
	}
}