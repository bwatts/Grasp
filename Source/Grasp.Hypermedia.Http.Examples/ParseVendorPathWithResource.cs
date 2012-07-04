using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http
{
	public class ParseVendorPathWithResource : Behavior
	{
		string _mediaType;
		VendorPath _parsedPath;

		protected override void Given()
		{
			_mediaType = "application/vnd.grasp.foo+xhtml";
		}

		protected override void When()
		{
			_parsedPath = VendorPath.Parse(_mediaType);
		}

		[Then]
		public void HasSpecifiedPath()
		{
			Assert.That(_parsedPath.Value, Is.EqualTo("vnd.grasp.foo+xhtml"));
		}

		[Then]
		public void HasSpecifiedName()
		{
			Assert.That(_parsedPath.Name, Is.EqualTo("grasp"));
		}

		[Then]
		public void HasSpecifiedResource()
		{
			Assert.That(_parsedPath.Resource, Is.EqualTo(new VendorResource("foo")));
		}

		[Then]
		public void HasKnownFormat()
		{
			Assert.That(_parsedPath.Format, Is.EqualTo(VendorFormat.Xhtml));
		}
	}
}