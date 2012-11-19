using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;

namespace Slate.Services
{
	[ContractClass(typeof(IFormMeshContract))]
	public interface IFormMesh
	{
		Uri GetFormLocation(EntityId formId);
	}

	[ContractClassFor(typeof(IFormMesh))]
	internal abstract class IFormMeshContract : IFormMesh
	{
		Uri IFormMesh.GetFormLocation(EntityId formId)
		{
			Contract.Requires(formId != EntityId.Unassigned);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}
	}
}