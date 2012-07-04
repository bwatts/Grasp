using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http
{
	public class CreateMediaTypeWithoutVendor : Behavior
	{
		MediaType _mediaType;

		protected override void Given()
		{}

		protected override void When()
		{
			_mediaType = new MediaType("application/json");
		}

		[Then]
		public void VendorPathIsUnspecified()
		{
			Assert.That(_mediaType.VendorPath, Is.EqualTo(VendorPath.Unspecified));
		}
	}
}