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
		Uri GetAbsoluteUrl(Uri relativeUrl);

		Uri GetAbsoluteUrl(string relativeUrl);

		HttpResourceHeader CreateHeader(string title);
	}

	[ContractClassFor(typeof(IHttpResourceContext))]
	internal abstract class IHttpResourceContextContract : IHttpResourceContext
	{
		Uri IHttpResourceContext.GetAbsoluteUrl(Uri relativeUri)
		{
			Contract.Requires(relativeUri != null);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}

		Uri IHttpResourceContext.GetAbsoluteUrl(string relativeUrl)
		{
			Contract.Requires(relativeUrl != null);
			Contract.Ensures(Contract.Result<Uri>() != null);

			return null;
		}

		HttpResourceHeader IHttpResourceContext.CreateHeader(string title)
		{
			Contract.Requires(title != null);
			Contract.Ensures(Contract.Result<HttpResourceHeader>() != null);

			return null;
		}
	}
}