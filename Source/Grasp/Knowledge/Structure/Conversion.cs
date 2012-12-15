using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Structure
{
	public sealed class Conversion : Notion, IConversion
	{
		public static readonly Field<Type> ConvertedTypeField = Field.On<Conversion>.For(x => x.ConvertedType);

		public Conversion(Type convertedType)
		{
			Contract.Requires(convertedType != null);

			ConvertedType = convertedType;
		}

		public Type ConvertedType { get { return GetValue(ConvertedTypeField); } private set { SetValue(ConvertedTypeField, value); } }

		public object Convert(object source)
		{
			var resultType = typeof(ConversionResult<>).MakeGenericType(ConvertedType);

			object converted;

			return ChangeType.TryTo(ConvertedType, source, out converted)
				? Activator.CreateInstance(resultType, converted)
				: Activator.CreateInstance(resultType);
		}
	}
}