using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media
{
	public class ParseVendorPathWithResourceWithoutFormat : Behavior
	{
		string _mediaType;
		VendorPath _parsedPath;

		protected override void Given()
		{
			_mediaType = "application/vnd.grasp.foo";
		}

		protected override void When()
		{
			_parsedPath = VendorPath.Parse(_mediaType);
		}

		[Then]
		public void IsUnspecified()
		{
			Assert.That(_parsedPath, Is.EqualTo(VendorPath.Unspecified));
		}
	}
}