using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class EnumValueModel : Notion
	{
		public static readonly Field<EnumModel> EnumModelField = Field.On<EnumValueModel>.For(x => x.EnumModel);
		public static readonly Field<string> NameField = Field.On<EnumValueModel>.For(x => x.Name);
		public static readonly Field<int> NumericValueField = Field.On<EnumValueModel>.For(x => x.NumericValue);
		public static readonly Field<object> ObjectValueField = Field.On<EnumValueModel>.For(x => x.ObjectValue);

		public EnumModel EnumModel { get { return GetValue(EnumModelField); } }
		public string Name { get { return GetValue(NameField); } }
		public object ObjectValue { get { return GetValue(ObjectValueField); } }
		public int NumericValue { get { return GetValue(NumericValueField); } }

		public override string ToString()
		{
			return Name;
		}
	}
}