using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics
{
	public class EnumModel : TypeModel
	{
		public static readonly Field<Many<EnumValueModel>> ValuesField = Field.On<EnumModel>.For(x => x.Values);

		public Many<EnumValueModel> Values { get { return GetValue(ValuesField); } }
	}
}