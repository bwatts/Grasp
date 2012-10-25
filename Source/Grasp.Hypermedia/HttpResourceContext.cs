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
		private readonly IEnumerable<HtmlLink> _links;

		public HttpResourceContext(Uri baseUrl, IEnumerable<HtmlLink> links = null)
		{
			Contract.Requires(baseUrl != null);
			Contract.Requires(baseUrl.IsAbsoluteUri);

			_baseUrl = baseUrl;
			_links = links ?? Enumerable.Empty<HtmlLink>();
		}

		public HttpResourceHeader CreateHeader(string title)
		{
			return new HttpResourceHeader(title, GetBaseLink(), _links);
		}

		private HtmlLink GetBaseLink()
		{
			return new HtmlLink(_baseUrl.ToString());
		}
	}
}