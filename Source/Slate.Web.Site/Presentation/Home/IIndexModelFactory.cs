using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Home
{
	[ContractClass(typeof(IIndexFactoryContract))]
	public interface IIndexModelFactory
	{
		Task<IndexModel> CreateIndexModelAsync(ListPageKey formPageKey, ListPageKey issuePageKey);
	}

	[ContractClassFor(typeof(IIndexModelFactory))]
	internal abstract class IIndexFactoryContract : IIndexModelFactory
	{
		Task<IndexModel> IIndexModelFactory.CreateIndexModelAsync(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			Contract.Requires(formPageKey != null);
			Contract.Requires(issuePageKey != null);
			Contract.Ensures(Contract.Result<Task<IndexModel>>() != null);

			return null;
		}
	}
}