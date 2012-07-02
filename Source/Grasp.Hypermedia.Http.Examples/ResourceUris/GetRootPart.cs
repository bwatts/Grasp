using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media.ResourceUris
{
	public class GetRootPart : Behavior
	{
		ResourceUriPart _root;

		protected override void Given()
		{}

		protected override void When()
		{
			_root = ResourceUriPart.Root;
		}

		[Then]
		public void ValueIsEmpty()
		{
			Assert.That(_root.Value, Is.EqualTo(""));
		}

		[Then]
		public void BasePartIsNull()
		{
			Assert.That(_root.BasePart, Is.Null);
		}
	}
}