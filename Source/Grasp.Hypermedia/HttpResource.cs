using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public abstract class HttpResource : Notion
	{
		public static readonly Field<MHeader> HeaderField = Field.On<HttpResource>.For(x => x.Header);

		protected HttpResource(MHeader header)
		{
			Contract.Requires(header != null);

			Header = header;
		}

		public MHeader Header { get { return GetValue(HeaderField); } private set { SetValue(HeaderField, value); } }
	}
}