using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Lists
{
	/// <summary>
	/// Describes an application service which fetches pages within lists of persistent items
	/// </summary>
	[ContractClass(typeof(IListServiceContract))]
	public interface IListService
	{
		/// <summary>
		/// Gets the page of the list identified by the specified key
		/// </summary>
		/// <param name="key">Identifies the page of the list to get, or the default if none is specified</param>
		/// <returns>The work of calculating the list page</returns>
		Task<ListPage> GetPageAsync(ListPageKey key = null);
	}

	[ContractClassFor(typeof(IListService))]
	internal abstract class IListServiceContract : IListService
	{
		Task<ListPage> IListService.GetPageAsync(ListPageKey key)
		{
			Contract.Ensures(Contract.Result<Task<ListPage>>() != null);

			return null;
		}
	}
}