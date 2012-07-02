using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media.ResourceUris
{
	public class RootPartThenText : Behavior
	{
		ResourceUriPart _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = ResourceUriPart.Root.Then("nextPart");
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(ResourceUriPart.Root));
		}

		[Then]
		public void HasSpecifiedNextPart()
		{
			Assert.That(_result.Value, Is.EqualTo("nextPart"));
		}
	}
}