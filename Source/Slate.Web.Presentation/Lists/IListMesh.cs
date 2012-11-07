using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Presentation.Lists
{
	[ContractClass(typeof(IListMeshContract))]
	public interface IListMesh
	{
		Hyperlink GetCountLink(Hyperlist list);

		Hyperlink GetPageLink(Hyperlist list, Number number);

		Hyperlink GetItemLink(Hyperlist list, HyperlistItem item);
	}

	[ContractClassFor(typeof(IListMesh))]
	internal abstract class IListMeshContract : IListMesh
	{
		Hyperlink IListMesh.GetCountLink(Hyperlist list)
		{
			Contract.Requires(list != null);
			Contract.Ensures(Contract.Result<Hyperlink>() != null);

			return null;
		}

		Hyperlink IListMesh.GetPageLink(Hyperlist list, Number number)
		{
			Contract.Requires(list != null);
			Contract.Requires(number != Number.None);
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
	}
}