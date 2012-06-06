using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics
{
	public class EnumValueModel : Notion
	{
		public static Field<EnumModel> EnumModelField = Field.On<EnumValueModel>.Backing(x => x.EnumModel);
		public static Field<string> NameField = Field.On<EnumValueModel>.Backing(x => x.Name);
		public static Field<int> NumericValueField = Field.On<EnumValueModel>.Backing(x => x.NumericValue);
		public static Field<object> ObjectValueField = Field.On<EnumValueModel>.Backing(x => x.ObjectValue);

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