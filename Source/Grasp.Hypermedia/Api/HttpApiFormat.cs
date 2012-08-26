using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia.Api
{
	public sealed class HttpApiFormat : MediaFormat<HttpApi>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.hypermedia.api");

		public HttpApiFormat() : base(MediaType)
		{}

		protected override HttpApi ConvertFromRepresentation(MRepresentation representation)
		{
			throw new NotImplementedException();
		}

		protected override MRepresentation ConvertToRepresentation(HttpApi media)
		{
			throw new NotImplementedException();
		}
	}
}