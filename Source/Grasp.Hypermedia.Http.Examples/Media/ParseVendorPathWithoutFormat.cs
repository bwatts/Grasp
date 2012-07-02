using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media
{
	public class ParseVendorPathWithoutFormat : Behavior
	{
		VendorPath _parsedPath;

		protected override void Given()
		{}

		protected override void When()
		{
			_parsedPath = VendorPath.Parse("application/vnd.grasp");
		}

		[Then]
		public void IsUnspecified()
		{
			Assert.That(_parsedPath, Is.EqualTo(VendorPath.Unspecified));
		}
	}
}