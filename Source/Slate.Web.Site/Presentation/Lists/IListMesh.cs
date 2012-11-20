using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	[ContractClass(typeof(IListMeshContract))]
	public interface IListMesh
	{
		Hyperlink GetPageLink(Hyperlist list, Count number);

		Hyperlink GetItemCountLink(Hyperlist list);

		Hyperlink GetItemLink(Hyperlist list, HyperlistItem item);

		Hyperlink GetItemNumberLink(Hyperlist list, HyperlistItem item);
	}

	[ContractClassFor(typeof(IListMesh))]
	internal abstract class IListMeshContract : IListMesh
	{
		Hyperlink IListMesh.GetPageLink(Hyperlist list, Count number)
		{
			Contract.Requires(list != null);
			Contract.Requires(number != Count.None);
			Contract.Ensures(Contract.Result<Hyperlink>() != null);

			return null;
		}

		Hyperlink IListMesh.GetItemCountLink(Hyperlist list)
		{
			Contract.Requires(list != null);
			Contract.Ensures(Contract.Result<Hyperlink>() != null);

			return null;
		}

		Hyperlink IListMesh.GetItemLink(Hyperlist list, HyperlistItem item)
		{
			Contract.Requires(list != null);
			Contract.Requires(item != null);
			Contract.Ensures(Contract.Result<Hyperlink>() != null);

			return null;
		}

		Hyperlink IListMesh.GetItemNumberLink(Hyperlist list, HyperlistItem item)
		{
			Contract.Requires(list != null);
			Contract.Requires(item != null);
			Contract.Ensures(Contract.Result<Hyperlink>() != null);

			return null;
		}
	}
}