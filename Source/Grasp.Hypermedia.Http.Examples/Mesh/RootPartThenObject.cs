using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootPartThenObject : Behavior
	{
		UriPart _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = UriPart.Root.Then(1);
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(UriPart.Root));
		}

		[Then]
		public void ValueIsObjectText()
		{
			Assert.That(_result.Value, Is.EqualTo("1"));
		}
	}
}