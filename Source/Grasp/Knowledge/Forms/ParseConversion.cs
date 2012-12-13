using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Knowledge.Structure;

namespace Grasp.Knowledge.Forms
{
	public sealed class ParseConversion : Notion, IConversion
	{
		public static readonly Field<Type> ConvertedTypeField = Field.On<ParseConversion>.For(x => x.ConvertedType);

		public ParseConversion(Type convertedType)
		{
			Contract.Requires(convertedType != null);

			ConvertedType = convertedType;
		}

		public Type ConvertedType { get { return GetValue(ConvertedTypeField); } private set { SetValue(ConvertedTypeField, value); } }

		public object Convert(object source)
		{
			var resultType = typeof(ConversionResult<>).MakeGenericType(ConvertedType);

			object converted;

			return Conversion.Try(ConvertedType, source, out converted)
				? Activator.CreateInstance(resultType, converted)
				: Activator.CreateInstance(resultType);
		}
	}
}