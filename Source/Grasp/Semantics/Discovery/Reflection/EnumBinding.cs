using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class EnumBinding : TypeBinding
	{
		public override TypeModel GetTypeModel()
		{
			var x = new EnumModel();

			TypeModel.TypeField.Set(x, Type);
			EnumModel.ValuesField.Set(x, new Many<EnumValueModel>(GetValues(x)));

			return x;
		}

		private IEnumerable<EnumValueModel> GetValues(EnumModel enumModel)
		{
			foreach(var value in Enum.GetValues(Type))
			{
				var x = new EnumValueModel();

				EnumValueModel.EnumModelField.Set(x, enumModel);
				EnumValueModel.NameField.Set(x, value.ToString());
				EnumValueModel.NumericValueField.Set(x, (int) value);
				EnumValueModel.ObjectValueField.Set(x, value);

				yield return x;
			}
		}
	}
}