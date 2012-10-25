using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Lists;

namespace Slate.Web.Presentation.Lists
{
	[ContractClass(typeof(IListFactoryContract))]
	public interface IListFactory
	{
		Task<ListModel> CreateListAsync(ListPageKey pageKey, Func<ListItem, object> itemIdSelector);
	}

	[ContractClassFor(typeof(IListFactory))]
	internal abstract class IListFactoryContract : IListFactory
	{
		Task<ListModel> IListFactory.CreateListAsync(ListPageKey pageKey, Func<ListItem, object> itemIdSelector)
		{
			Contract.Requires(pageKey != null);
			Contract.Requires(itemIdSelector != null);
			Contract.Ensures(Contract.Result<Task<ListModel>>() != null);

			return null;
		}
	}
}