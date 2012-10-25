using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;

namespace Slate.Web.Presentation.Lists
{
	[ContractClass(typeof(IListMeshContract))]
	public interface IListMesh
	{
		HtmlLink GetCountLink(ListPage page);

		HtmlLink GetPageLink(ListPage page, Number number);

		HtmlLink GetItemLink(ListPage page, Number number);

		HtmlLink GetItemLink(object id);
	}

	[ContractClassFor(typeof(IListMesh))]
	internal abstract class IListMeshContract : IListMesh
	{
		HtmlLink IListMesh.GetCountLink(ListPage page)
		{
			Contract.Requires(page != null);
			Contract.Ensures(Contract.Result<HtmlLink>() != null);

			return null;
		}

		HtmlLink IListMesh.GetPageLink(ListPage page, Number number)
		{
			Contract.Requires(page != null);
			Contract.Requires(number != Number.None);
			Contract.Ensures(Contract.Result<HtmlLink>() != null);

			return null;
		}

		HtmlLink IListMesh.GetItemLink(ListPage page, Number number)
		{
			Contract.Requires(page != null);
			Contract.Requires(number != Number.None);
			Contract.Ensures(Contract.Result<HtmlLink>() != null);

			return null;
		}

		HtmlLink IListMesh.GetItemLink(object id)
		{
			Contract.Requires(id != null);
			Contract.Ensures(Contract.Result<HtmlLink>() != null);

			return null;
		}
	}
}