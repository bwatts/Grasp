using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http
{
	public class CreateMediaTypeWithVendor : Behavior
	{
		MediaType _mediaType;

		protected override void Given()
		{}

		protected override void When()
		{
			_mediaType = new MediaType("application/vnd.grasp.test");
		}

		[Then]
		public void VendorPathHasSpecifiedValue()
		{
			Assert.That(_mediaType.VendorPath.Value, Is.EqualTo("vnd.grasp.test"));
		}

		[Then]
		public void VendorPathHasSpecifiedVendor()
		{
			Assert.That(_mediaType.VendorPath.Vendor, Is.EqualTo("grasp"));
		}

		[Then]
		public void VendorPathHasSpecifiedMedia()
		{
			Assert.That(_mediaType.VendorPath.Media, Is.EqualTo("test"));
		}
	}
}