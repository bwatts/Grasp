using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Work
{
	[ContractClass(typeof(IWorkMeshContract))]
	public interface IWorkMesh
	{
		Uri GetItemUri(WorkItemResource apiItem);

		Uri GetResultUrl(WorkItemResource apiItem);
	}

	[ContractClassFor(typeof(IWorkMesh))]
	internal abstract class IWorkMeshContract : IWorkMesh
	{
		Uri IWorkMesh.GetItemUri(WorkItemResource apiItem)
		{
			Contract.Requires(apiItem != null);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}

		Uri IWorkMesh.GetResultUrl(WorkItemResource apiItem)
		{
			Contract.Requires(apiItem != null);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}
	}
}