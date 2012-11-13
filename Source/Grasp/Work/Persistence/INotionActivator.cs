using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Persistence
{
	/// <summary>
	/// Describes the activation process of objects derived from <see cref="Notion"/>
	/// </summary>
	[ContractClass(typeof(INotionActivatorContract))]
	public interface INotionActivator
	{
		bool CanActivate(Type type);

		Notion Activate(Type type, INotionState state = null);

		T Activate<T>(INotionState state = null) where T : Notion;
	}

	[ContractClassFor(typeof(INotionActivator))]
	internal abstract class INotionActivatorContract : INotionActivator
	{
		bool INotionActivator.CanActivate(Type type)
		{
			Contract.Requires(type != null);

			return false;
		}

		Notion INotionActivator.Activate(Type type, INotionState state)
		{
			Contract.Requires(type != null);
			Contract.Ensures(Contract.Result<Notion>() != null);

			return null;
		}

		T INotionActivator.Activate<T>(INotionState state)
		{
			Contract.Ensures(Contract.Result<T>() != null);

			return null;
		}
	}
}