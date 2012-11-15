using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slate.Services
{
	[ContractClass(typeof(IFormMeshContract))]
	public interface IFormMesh
	{
		Uri GetFormLocation(Guid formId);
	}

	[ContractClassFor(typeof(IFormMesh))]
	internal abstract class IFormMeshContract : IFormMesh
	{
		Uri IFormMesh.GetFormLocation(Guid formId)
		{
			Contract.Requires(formId != Guid.Empty);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}
	}
}