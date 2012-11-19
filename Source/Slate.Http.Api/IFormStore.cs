using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Http.Api
{
	[ContractClass(typeof(IFormStoreContract))]
	public interface IFormStore
	{
		Task<Hyperlist> GetListAsync(ListPageKey pageKey = null);

		Task<FormResource> GetFormAsync(EntityId id);
	}

	[ContractClassFor(typeof(IFormStore))]
	internal abstract class IFormStoreContract : IFormStore
	{
		Task<Hyperlist> IFormStore.GetListAsync(ListPageKey pageKey)
		{
			Contract.Requires(pageKey != null);
			Contract.Ensures(Contract.Result<Task<Hyperlist>>() != null);

			return null;
		}

		Task<FormResource> IFormStore.GetFormAsync(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);

			return null;
		}
	}
}