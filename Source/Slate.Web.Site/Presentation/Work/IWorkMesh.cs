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
		Uri GetResultUrl(WorkItemResource item);
	}

	[ContractClassFor(typeof(IWorkMesh))]
	internal abstract class IWorkMeshContract : IWorkMesh
	{
		Uri IWorkMesh.GetResultUrl(WorkItemResource item)
		{
			Contract.Requires(item != null);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}
	}
}