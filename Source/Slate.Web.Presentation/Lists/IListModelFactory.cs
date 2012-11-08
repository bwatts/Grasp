﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Presentation.Lists
{
	[ContractClass(typeof(IListModelFactoryContract))]
	public interface IListModelFactory
	{
		Task<ListModel> CreateListModelAsync(Uri uri, ListPageKey pageKey, Func<HyperlistItem, object> itemIdSelector);
	}

	[ContractClassFor(typeof(IListModelFactory))]
	internal abstract class IListModelFactoryContract : IListModelFactory
	{
		Task<ListModel> IListModelFactory.CreateListModelAsync(Uri uri, ListPageKey pageKey, Func<HyperlistItem, object> itemIdSelector)
		{
			Contract.Requires(uri != null);
			Contract.Requires(pageKey != null);
			Contract.Requires(itemIdSelector != null);
			Contract.Ensures(Contract.Result<Task<ListModel>>() != null);

			return null;
		}
	}
}