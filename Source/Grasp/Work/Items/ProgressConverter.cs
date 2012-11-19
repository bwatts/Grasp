using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Work.Items
{
	public class ProgressConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(double) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(double) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value is string)
			{
				return ConvertFromString(culture, (string) value);
			}
			else if(value is double)
			{
				return new Progress((double) value);
			}
			else
			{
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if(value is Progress)
			{
				var progress = (Progress) value;

				if(destinationType == typeof(string))
				{
					return progress.ToString();
				}
				else
				{
					if(destinationType == typeof(double))
					{
						return progress.Value;
					}
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}

		private static Progress ConvertFromString(CultureInfo culture, string value)
		{
			var percentSymbol = culture.NumberFormat.PercentSymbol;

			var isPercentage = value.EndsWith(percentSymbol);

			if(isPercentage)
			{
				value = value.Substring(0, value.Length - percentSymbol.Length);
			}

			var percentage = Conversion.To<double>(value);

			if(isPercentage)
			{
				percentage /= 100;
			}

			return new Progress(percentage);
		}
	}
}