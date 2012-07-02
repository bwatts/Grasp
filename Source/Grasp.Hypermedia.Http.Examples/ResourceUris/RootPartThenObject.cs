using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media.ResourceUris
{
	public class RootPartThenObject : Behavior
	{
		ResourceUriPart _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = ResourceUriPart.Root.Then(1);
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(ResourceUriPart.Root));
		}

		[Then]
		public void ValueIsObjectText()
		{
			Assert.That(_result.Value, Is.EqualTo("1"));
		}
	}
}