using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia
{
	public sealed class HttpResourceContext : IHttpResourceContext
	{
		private readonly Uri _baseUrl;
		private readonly IEnumerable<Hyperlink> _links;

		public HttpResourceContext(Uri baseUrl, IEnumerable<Hyperlink> links = null)
		{
			Contract.Requires(baseUrl != null);
			Contract.Requires(baseUrl.IsAbsoluteUri);

			_baseUrl = baseUrl;
			_links = links ?? Enumerable.Empty<Hyperlink>();
		}

		public HttpResourceHeader CreateHeader(string title)
		{
			return new HttpResourceHeader(title, GetBaseLink(), _links);
		}

		private Hyperlink GetBaseLink()
		{
			return new Hyperlink(_baseUrl.ToString());
		}
	}
}