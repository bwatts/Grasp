using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	/// <summary>
	/// Converts instances of <see cref="RevisionId"/> to and from <see cref="String"/> and <see cref="Int64"/> values
	/// </summary>
	public class RevisionIdConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(long) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(long) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value is string)
			{
				return new RevisionId((string) value);
			}
			else if(value is long)
			{
				return new RevisionId((long) value);
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
				return ((RevisionId) value).ToString();
			}
			else if(destinationType == typeof(long))
			{
				return ((RevisionId) value).ToInt64();
			}
			else if(destinationType == typeof(RevisionId))
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