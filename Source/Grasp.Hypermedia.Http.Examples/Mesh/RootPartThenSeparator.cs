using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootPartThenSeparator : Behavior
	{
		UriPart _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = UriPart.Root.Then(UriPart.Separator);
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(UriPart.Root));
		}

		[Then]
		public void ValueIsEmpty()
		{
			Assert.That(_result.Value, Is.EqualTo(""));
		}
	}
}