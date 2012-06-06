using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics
{
	public class EnumModel : TypeModel
	{
		public static Field<Many<EnumValueModel>> ValuesField = Field.On<EnumModel>.Backing(x => x.Values);

		public Many<EnumValueModel> Values { get { return GetValue(ValuesField); } }
	}
}