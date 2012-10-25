using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia
{
	[ContractClass(typeof(IHttpResourceContextContract))]
	public interface IHttpResourceContext
	{
		HttpResourceHeader CreateHeader(string title);
	}

	[ContractClassFor(typeof(IHttpResourceContext))]
	internal abstract class IHttpResourceContextContract : IHttpResourceContext
	{
		HttpResourceHeader IHttpResourceContext.CreateHeader(string title)
		{
			Contract.Requires(title != null);
			Contract.Ensures(Contract.Result<HttpResourceHeader>() != null);

			return null;
		}
	}
}