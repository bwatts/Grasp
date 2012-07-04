using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http
{
	public class ParseVendorPath : Behavior
	{
		string _mediaType;
		VendorPath _parsedPath;

		protected override void Given()
		{
			_mediaType = "application/vnd.grasp+xhtml";
		}

		protected override void When()
		{
			_parsedPath = VendorPath.Parse(_mediaType);
		}

		[Then]
		public void HasSpecifiedPath()
		{
			Assert.That(_parsedPath.Value, Is.EqualTo("vnd.grasp+xhtml"));
		}

		[Then]
		public void HasSpecifiedName()
		{
			Assert.That(_parsedPath.Name, Is.EqualTo("grasp"));
		}

		[Then]
		public void HasUnspecifiedResource()
		{
			Assert.That(_parsedPath.Resource, Is.EqualTo(VendorResource.Unspecified));
		}

		[Then]
		public void HasKnownFormat()
		{
			Assert.That(_parsedPath.Format, Is.EqualTo(VendorFormat.Xhtml));
		}
	}
}