using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slate.Forms;

namespace Slate.Http.Api
{
	[ContractClass(typeof(IFormByIdQueryContract))]
	public interface IFormByIdQuery
	{
		Task<Form> GetFormAsync(Guid id);
	}

	[ContractClassFor(typeof(IFormByIdQuery))]
	internal abstract class IFormByIdQueryContract : IFormByIdQuery
	{
		Task<Form> IFormByIdQuery.GetFormAsync(Guid id)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Ensures(Contract.Result<Task<Form>>() != null);

			return null;
		}
	}
}