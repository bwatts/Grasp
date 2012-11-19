using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp
{
	public class EntityIdConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Guid) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(Guid) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value is string)
			{
				return new EntityId((string) value);
			}
			else if(value is Guid)
			{
				return new EntityId((Guid) value);
			}
			else
			{
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if(destinationType == typeof(string))
			{
				return ((EntityId) value).ToString();
			}
			else if(destinationType == typeof(Guid))
			{
				return ((EntityId) value).Value;
			}
			else if(destinationType == typeof(EntityId))
			{
				return ConvertFrom(context, culture, value);
			}
			else
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}