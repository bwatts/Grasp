using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	[ContractClass(typeof(INotionActivatorContract))]
	public interface INotionActivator
	{
		Notion ActivateUninitializedNotion(Type type);

		T ActivateUninitializedNotion<T>() where T : Notion;
	}

	[ContractClassFor(typeof(INotionActivator))]
	internal abstract class INotionActivatorContract : INotionActivator
	{
		Notion INotionActivator.ActivateUninitializedNotion(Type type)
		{
			Contract.Requires(type != null);
			Contract.Requires(typeof(Notion).IsAssignableFrom(type));
			Contract.Ensures(Contract.Result<Notion>() != null);

			return null;
		}

		T INotionActivator.ActivateUninitializedNotion<T>()
		{
			Contract.Ensures(Contract.Result<T>() != null);

			return null;
		}
	}
}