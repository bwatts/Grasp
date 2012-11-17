using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	[ContractClass(typeof(IHttpResourceContextContract))]
	public interface IHttpResourceContext
	{
		Uri GetAbsoluteUrl(Uri relativeUrl);

		Uri GetAbsoluteUrl(string relativeUrl);

		MHeader CreateHeader(string title, Uri selfUri);

		MHeader CreateHeader(string title, string selfUri);
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

		MHeader IHttpResourceContext.CreateHeader(string title, Uri selfUri)
		{
			Contract.Requires(title != null);
			Contract.Requires(selfUri != null);
			Contract.Ensures(Contract.Result<MHeader>() != null);

			return null;
		}

		MHeader IHttpResourceContext.CreateHeader(string title, string selfUri)
		{
			Contract.Requires(title != null);
			Contract.Requires(selfUri != null);
			Contract.Ensures(Contract.Result<MHeader>() != null);

			return null;
		}
	}
}