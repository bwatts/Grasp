using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media.ResourceUris
{
	public class RootPartToAbsoluteUri : Behavior
	{
		Uri _baseUrl;
		Uri _url;

		protected override void Given()
		{
			_baseUrl = new Uri("http://www.grasp.com");
		}

		protected override void When()
		{
			_url = ResourceUriPart.Root.ToAbsoluteUrl(_baseUrl);
		}

		[Then]
		public void IsBaseUrl()
		{
			Assert.That(_url, Is.EqualTo(_baseUrl));
		}
	}
}