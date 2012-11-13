using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Work.Persistence
{
	public sealed class FieldValueConverter : Notion, IFieldValueConverter
	{
		public object ConvertValueToFieldType(Type fieldType, object value)
		{
			return fieldType.IsEnum ? ConvertValueToEnum(fieldType, value) : ConvertValue(fieldType, value);
		}

		private static object ConvertValueToEnum(Type fieldType, object value)
		{
			var text = value as string;

			return text != null ? Enum.Parse(fieldType, text) : Enum.ToObject(fieldType, value);
		}

		private static object ConvertValue(Type fieldType, object value)
		{
			return ObjectConverter.To(fieldType, value);
		}
	}
}