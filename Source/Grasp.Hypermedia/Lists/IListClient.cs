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
		/// <param name="viewKey">The key of the view to fetch</param>
		/// <returns>The work of fetching the list</returns>
		Task<Hyperlist> GetListAsync(Uri uri, ListViewKey viewKey);
	}

	[ContractClassFor(typeof(IListClient))]
	internal abstract class IListClientContract : IListClient
	{
		Task<Hyperlist> IListClient.GetListAsync(Uri uri, ListViewKey viewKey)
		{
			Contract.Requires(uri != null);
			Contract.Requires(viewKey != null);
			Contract.Ensures(Contract.Result<Task<Hyperlist>>() != null);

			return null;
		}
	}
}