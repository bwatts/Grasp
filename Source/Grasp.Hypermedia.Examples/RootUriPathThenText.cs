using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia
{
	public class RootUriPathThenText : Behavior
	{
		UriPath _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = UriPath.Root.Then("nextPart");
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePath, Is.EqualTo(UriPath.Root));
		}

		[Then]
		public void HasSpecifiedNextPart()
		{
			Assert.That(_result.Value, Is.EqualTo("nextPart"));
		}
	}
}