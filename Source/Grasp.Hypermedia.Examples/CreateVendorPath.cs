using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http
{
	public class CreateVendorPath : Behavior
	{
		VendorPath _path;

		protected override void Given()
		{}

		protected override void When()
		{
			_path = new VendorPath("application/vnd.grasp.test");
		}

		[Then]
		public void HasSpecifiedValue()
		{
			Assert.That(_path.Value, Is.EqualTo("vnd.grasp.test"));
		}

		[Then]
		public void HasSpecifiedVendor()
		{
			Assert.That(_path.Vendor, Is.EqualTo("grasp"));
		}

		[Then]
		public void HasSpecifiedMedia()
		{
			Assert.That(_path.Media, Is.EqualTo("test"));
		}
	}
}