using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	[ContractClass(typeof(IApiSessionContract))]
	public interface IApiSession
	{
		Task<Uri> GetEntitySetUrlAsync(MClass @class, bool throwIfEntityUnsupported = true);

		Task<Uri> GetEntityUrlAsync(MClass @class, string id, bool throwIfEntityUnsupported = true);
	}

	[ContractClassFor(typeof(IApiSession))]
	internal abstract class IApiSessionContract : IApiSession
	{
		Task<Uri> IApiSession.GetEntitySetUrlAsync(MClass @class, bool throwIfEntityUnsupported)
		{
			Contract.Requires(@class != null);
			Contract.Ensures(Contract.Result<Task<Uri>>() != null);

			return null;
		}

		Task<Uri> IApiSession.GetEntityUrlAsync(MClass @class, string id, bool throwIfEntityUnsupported)
		{
			Contract.Requires(@class != null);
			Contract.Requires(id != null);
			Contract.Ensures(Contract.Result<Task<Uri>>() != null);

			return null;
		}
	}
}