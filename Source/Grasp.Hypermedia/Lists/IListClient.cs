using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	/// <summary>
	/// Describes a client which accesses pages of hypermedia lists
	/// </summary>
	[ContractClass(typeof(IListClientContract))]
	public interface IListClient
	{
		/// <summary>
		/// Gets the specified page of the hypermedia list
		/// </summary>
		/// <param name="uri">The URI of the hypermedia list endpoint</param>
		/// <param name="pageKey">The key of the page to fetch</param>
		/// <returns>The work of fetching the list</returns>
		Task<Hyperlist> GetListAsync(Uri uri, ListPageKey pageKey);
	}

	[ContractClassFor(typeof(IListClient))]
	internal abstract class IListClientContract : IListClient
	{
		Task<Hyperlist> IListClient.GetListAsync(Uri uri, ListPageKey pageKey)
		{
			Contract.Requires(uri != null);
			Contract.Requires(pageKey != null);
			Contract.Ensures(Contract.Result<Task<Hyperlist>>() != null);

			return null;
		}
	}
}