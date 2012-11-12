using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia
{
	public sealed class HttpResourceContext : Notion, IHttpResourceContext
	{
		public static readonly Field<Uri> _baseUrlField = Field.On<HttpResourceContext>.For(x => x._baseUrl);
		public static readonly Field<ManyInOrder<Hyperlink>> _linksField = Field.On<HttpResourceContext>.For(x => x._links);

		private Uri _baseUrl { get { return GetValue(_baseUrlField); } set { SetValue(_baseUrlField, value); } }
		private ManyInOrder<Hyperlink> _links { get { return GetValue(_linksField); } set { SetValue(_linksField, value); } }

		public HttpResourceContext(Uri baseUrl, IEnumerable<Hyperlink> links = null)
		{
			Contract.Requires(baseUrl != null);
			Contract.Requires(baseUrl.IsAbsoluteUri);

			_baseUrl = baseUrl;
			_links = (links ?? Enumerable.Empty<Hyperlink>()).ToManyInOrder();
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