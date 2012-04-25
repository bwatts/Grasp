using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Base implementation of a method that produces check rules which apply to a set of types
	/// </summary>
	[ContractClass(typeof(MultiTypeCheckMethodContract))]
	public abstract class MultiTypeCheckMethod : CheckMethod
	{
		/// <summary>
		/// Determines whether the specified target type is assignable to any of the supported types of this check method
		/// </summary>
		/// <param name="targetType">The type to check for support</param>
		/// <returns>Whether the specified target type is assignable to any of the supported types of this check method</returns>
		protected override bool SupportsTargetType(Type targetType)
		{
			return TargetTypes.Any(supportedType => supportedType.IsAssignableFrom(targetType));
		}

		/// <summary>
		/// When implemented by a derived class, gets the types supported by this check method
		/// </summary>
		protected abstract IEnumerable<Type> TargetTypes { get; }
	}

	[ContractClassFor(typeof(MultiTypeCheckMethod))]
	internal abstract class MultiTypeCheckMethodContract : MultiTypeCheckMethod
	{
		protected override IEnumerable<Type> TargetTypes
		{
			get
			{
				Contract.Ensures(Contract.Result<IEnumerable<Type>>() != null);

				return null;
			}
		}
	}
}