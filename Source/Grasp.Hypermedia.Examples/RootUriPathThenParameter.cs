using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia
{
	public class RootUriPathThenParameter : Behavior
	{
		UriPath _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = UriPath.Root.ThenParameter("p");
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePath, Is.EqualTo(UriPath.Root));
		}

		[Then]
		public void ValueIsParameterParameterNameInBraces()
		{
			Assert.That(_result.Value, Is.EqualTo("{p}"));
		}
	}
}