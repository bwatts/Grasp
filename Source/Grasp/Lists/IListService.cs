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
		/// Gets the view of the list identified by the specified key
		/// </summary>
		/// <param name="key">Identifies the view of the list to get, or the default if none is specified</param>
		/// <returns>The work of calculating the list view</returns>
		Task<ListView> GetViewAsync(ListViewKey key = null);
	}

	[ContractClassFor(typeof(IListService))]
	internal abstract class IListServiceContract : IListService
	{
		Task<ListView> IListService.GetViewAsync(ListViewKey key)
		{
			Contract.Ensures(Contract.Result<Task<ListView>>() != null);

			return null;
		}
	}
}