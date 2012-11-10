using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class EnumModel : TypeModel
	{
		public static readonly Field<Many<EnumValueModel>> ValuesField = Field.On<EnumModel>.For(x => x.Values);

		public EnumModel(Type type, IEnumerable<EnumValueModel> values) : base(type)
		{
			Contract.Requires(type.IsEnum);
			Contract.Requires(values != null);

			Values = values.ToMany();
		}

		public Many<EnumValueModel> Values { get { return GetValue(ValuesField); } private set { SetValue(ValuesField, value); } }
	}
}