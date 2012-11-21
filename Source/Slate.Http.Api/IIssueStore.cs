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
	[ContractClass(typeof(IIssueStoreContract))]
	public interface IIssueStore
	{
		Task<Hyperlist> GetListAsync(HyperlistQuery query);
	}

	[ContractClassFor(typeof(IIssueStore))]
	internal abstract class IIssueStoreContract : IIssueStore
	{
		Task<Hyperlist> IIssueStore.GetListAsync(HyperlistQuery query)
		{
			Contract.Requires(query != null);
			Contract.Ensures(Contract.Result<Task<Hyperlist>>() != null);

			return null;
		}
	}
}