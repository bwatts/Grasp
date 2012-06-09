using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia.Http.Media
{
	[AttributeUsage(AttributeTargets.Assembly)]
	public class MediaFormatAttribute : Attribute
	{
		public MediaFormatAttribute(string suffix)
		{
			Contract.Requires(suffix != null);

			Suffix = suffix;
		}

		public string Suffix { get; private set; }
	}
}