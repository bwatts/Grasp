using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia.Api
{
	public sealed class MediaTypeFormat : MediaFormat<MediaType>
	{
		public static readonly MediaType MediaType = new MediaType("application/vnd.grasp.hypermedia.media-type");

		public MediaTypeFormat() : base(MediaType)
		{}

		protected override MediaType ConvertFromRepresentation(MRepresentation representation)
		{
			throw new NotImplementedException();
		}

		protected override MRepresentation ConvertToRepresentation(MediaType media)
		{
			throw new NotImplementedException();
		}
	}
}