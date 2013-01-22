using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Semantics;

namespace Grasp.Persistence
{
	/// <summary>
	/// Describes the state of a notion in a domain model
	/// </summary>
	[ContractClass(typeof(INotionStateContract))]
	public interface INotionState
	{
		/// <summary>
		/// Gets the effective type of the corresponding notion by considering the expected type and anything specified by this state
		/// </summary>
		/// <returns>The effective type of the corresponding notion</returns>
		Type GetEffectiveType(Type expectedType);

		/// <summary>
		/// Gets the bindings applicable to the specified notion
		/// </summary>
		/// <param name="domainModel">The definition of the domain containing the notion</param>
		/// <param name="model">The definition of the notion which will receive this state</param>
		/// <param name="activator">Activates instances of sub-notions</param>
		/// <returns>The bindings described by this state for the specified notion</returns>
		IEnumerable<FieldBinding> GetBindings(DomainModel domainModel, NotionModel model, INotionActivator activator);
	}

	[ContractClassFor(typeof(INotionState))]
	internal abstract class INotionStateContract : INotionState
	{
		Type INotionState.GetEffectiveType(Type expectedType)
		{
			Contract.Requires(expectedType != null);
			Contract.Ensures(Contract.Result<Type>() != null);

			return null;
		}

		IEnumerable<FieldBinding> INotionState.GetBindings(DomainModel domainModel, NotionModel model, INotionActivator activator)
		{
			Contract.Requires(domainModel != null);
			Contract.Requires(model != null);
			Contract.Requires(activator != null);
			Contract.Ensures(Contract.Result<IEnumerable<FieldBinding>>() != null);

			return null;
		}
	}
}