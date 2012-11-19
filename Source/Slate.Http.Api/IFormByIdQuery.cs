using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Slate.Forms;

namespace Slate.Http.Api
{
	[ContractClass(typeof(IFormByIdQueryContract))]
	public interface IFormByIdQuery
	{
		Task<Form> GetFormAsync(EntityId id);
	}

	[ContractClassFor(typeof(IFormByIdQuery))]
	internal abstract class IFormByIdQueryContract : IFormByIdQuery
	{
		Task<Form> IFormByIdQuery.GetFormAsync(EntityId id)
		{
			Contract.Requires(id != EntityId.Unassigned);
			Contract.Ensures(Contract.Result<Task<Form>>() != null);

			return null;
		}
	}
}