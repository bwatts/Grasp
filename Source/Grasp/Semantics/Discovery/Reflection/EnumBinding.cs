using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class EnumBinding : TypeBinding
	{
		public override TypeModel GetTypeModel()
		{
			var x = new EnumModel();

			x.SetValue(TypeModel.TypeField, Type);
			x.SetValue(EnumModel.ValuesField, new Many<EnumValueModel>(GetValues(x)));

			return x;
		}

		private IEnumerable<EnumValueModel> GetValues(EnumModel enumModel)
		{
			foreach(var value in Enum.GetValues(Type))
			{
				var x = new EnumValueModel();

				x.SetValue(EnumValueModel.EnumModelField, enumModel);
				x.SetValue(EnumValueModel.NameField, value.ToString());
				x.SetValue(EnumValueModel.NumericValueField, (int) value);
				x.SetValue(EnumValueModel.ObjectValueField, value);

				yield return x;
			}
		}
	}
}