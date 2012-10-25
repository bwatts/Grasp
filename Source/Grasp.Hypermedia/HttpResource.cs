using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia
{
	public abstract class HttpResource : Notion
	{
		public static readonly Field<HttpResourceHeader> HeaderField = Field.On<HttpResource>.For(x => x.Header);

		public HttpResource(HttpResourceHeader header)
		{
			Contract.Requires(header != null);

			Header = header;
		}

		public HttpResourceHeader Header { get { return GetValue(HeaderField); } private set { SetValue(HeaderField, value); } }
	}
}