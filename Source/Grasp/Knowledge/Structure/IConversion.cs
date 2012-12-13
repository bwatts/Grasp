using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Knowledge.Structure
{
	[ContractClass(typeof(IConversionContract))]
	public interface IConversion
	{
		Type ConvertedType { get; }

		object Convert(object source);
	}

	[ContractClassFor(typeof(IConversion))]
	internal abstract class IConversionContract : IConversion
	{
		Type IConversion.ConvertedType
		{
			get
			{
				Contract.Ensures(Contract.Result<Type>() != null);

				return null;
			}
		}

		object IConversion.Convert(object source)
		{
			Contract.Ensures(Contract.Result<object>() != null);
			Contract.Ensures(Contract.Result<object>().GetType().IsAssignableToGenericDefinition(typeof(ConversionResult<>)));

			return null;
		}
	}
}