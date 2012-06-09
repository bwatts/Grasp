using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Http.Media
{
	public sealed class MediaTypePath : MediaTypePart<MediaTypePath>
	{
		public static readonly MediaTypePath Unspecified = new MediaTypePath("");

		public MediaTypePath(string value) : base(value)
		{}
	}
}