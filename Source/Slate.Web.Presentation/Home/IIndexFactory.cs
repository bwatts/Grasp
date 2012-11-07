using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Lists;

namespace Slate.Web.Presentation.Home
{
	[ContractClass(typeof(IIndexFactoryContract))]
	public interface IIndexFactory
	{
		Task<IndexModel> CreateIndexAsync(ListPageKey formPageKey, ListPageKey issuePageKey);
	}

	[ContractClassFor(typeof(IIndexFactory))]
	internal abstract class IIndexFactoryContract : IIndexFactory
	{
		Task<IndexModel> IIndexFactory.CreateIndexAsync(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			Contract.Requires(formPageKey != null);
			Contract.Requires(issuePageKey != null);
			Contract.Ensures(Contract.Result<Task<IndexModel>>() != null);

			return null;
		}
	}
}