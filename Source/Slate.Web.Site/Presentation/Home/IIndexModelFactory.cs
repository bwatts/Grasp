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
		Task<IndexModel> CreateIndexModelAsync(ListViewKey formPageKey, ListViewKey issuePageKey);
	}

	[ContractClassFor(typeof(IIndexModelFactory))]
	internal abstract class IIndexFactoryContract : IIndexModelFactory
	{
		Task<IndexModel> IIndexModelFactory.CreateIndexModelAsync(ListViewKey formPageKey, ListViewKey issuePageKey)
		{
			Contract.Requires(formPageKey != null);
			Contract.Requires(issuePageKey != null);
			Contract.Ensures(Contract.Result<Task<IndexModel>>() != null);

			return null;
		}
	}
}