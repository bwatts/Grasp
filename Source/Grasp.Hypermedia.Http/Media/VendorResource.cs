using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Http.Media
{
	public sealed class VendorResource : VendorPart<VendorResource>
	{
		public static readonly VendorResource Unspecified = new VendorResource("");

		public VendorResource(string value) : base(value)
		{}
	}
}