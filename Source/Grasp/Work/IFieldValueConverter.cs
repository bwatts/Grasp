using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	[ContractClass(typeof(IFieldValueConverterContract))]
	public interface IFieldValueConverter
	{
		object ConvertValueToFieldType(Type fieldType, object value);
	}

	[ContractClassFor(typeof(IFieldValueConverter))]
	internal abstract class IFieldValueConverterContract : IFieldValueConverter
	{
		object IFieldValueConverter.ConvertValueToFieldType(Type fieldType, object value)
		{
			Contract.Requires(fieldType != null);

			return null;
		}
	}
}