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
			_mediaType = new MediaType("application/vnd.grasp.foo+xhtml");
		}

		[Then]
		public void PathHasSpecifiedValue()
		{
			Assert.That(_mediaType.VendorPath.Value, Is.EqualTo("vnd.grasp.foo+xhtml"));
		}

		[Then]
		public void PathHasSpecifiedName()
		{
			Assert.That(_mediaType.VendorPath.Name, Is.EqualTo("grasp"));
		}

		[Then]
		public void PathHasSpecifiedResource()
		{
			Assert.That(_mediaType.VendorPath.Resource, Is.EqualTo(new VendorResource("foo")));
		}

		[Then]
		public void PathHasKnownFormat()
		{
			Assert.That(_mediaType.VendorPath.Format, Is.EqualTo(VendorFormat.Xhtml));
		}
	}
}