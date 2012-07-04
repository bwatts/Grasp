using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using Grasp.Hypermedia.Http.Mesh;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootPartThenText : Behavior
	{
		UriPart _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = UriPart.Root.Then("nextPart");
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(UriPart.Root));
		}

		[Then]
		public void HasSpecifiedNextPart()
		{
			Assert.That(_result.Value, Is.EqualTo("nextPart"));
		}
	}
}