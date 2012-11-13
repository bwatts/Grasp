using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Persistence
{
	/// <summary>
	/// Describes the conversion of values to the types of corresponding fields
	/// </summary>
	[ContractClass(typeof(IFieldValueConverterContract))]
	public interface IFieldValueConverter
	{
		/// <summary>
		/// Converts the specified value to the type of the specified field
		/// </summary>
		/// <param name="fieldType">The type to which to convert the value</param>
		/// <param name="value">The value to be converted to the specified type</param>
		/// <returns>The specified value after converting it to the specified field type</returns>
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