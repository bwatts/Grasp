using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Http.Media
{
	public sealed class MediaTypeFormat : MediaTypePart<MediaTypeFormat>
	{
		public static readonly MediaTypeFormat Unspecified = new MediaTypeFormat("");
		public static readonly MediaTypeFormat Json = new MediaTypeFormat("JSON");
		public static readonly MediaTypeFormat Xml = new MediaTypeFormat("XML");
		public static readonly MediaTypeFormat Xhtml = new MediaTypeFormat("XHTML");

		public MediaTypeFormat(string value) : base(value)
		{}
	}
}