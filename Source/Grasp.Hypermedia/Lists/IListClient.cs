using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia.Lists
{
	[ContractClass(typeof(IListClientContract))]
	public interface IListClient
	{
		Task<ListResource> GetListAsync(ListPageKey key);
	}

	[ContractClassFor(typeof(IListClient))]
	internal abstract class IListClientContract : IListClient
	{
		Task<ListResource> IListClient.GetListAsync(ListPageKey key)
		{
			Contract.Requires(key != null);
			Contract.Ensures(Contract.Result<Task<ListResource>>() != null);

			return null;
		}
	}
}