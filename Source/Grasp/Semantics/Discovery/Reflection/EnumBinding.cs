using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery.Reflection
{
	public class EnumBinding : TypeBinding
	{
		public EnumBinding(Type type) : base(type)
		{
			Contract.Requires(type.IsEnum);
		}

		public override TypeModel GetTypeModel()
		{
			return new EnumModel(Type, GetValues());
		}

		private IEnumerable<EnumValueModel> GetValues()
		{
			return Enum.GetValues(Type).Cast<object>().Select(value => new EnumValueModel(value.ToString(), (int) value, value));
		}
	}
}