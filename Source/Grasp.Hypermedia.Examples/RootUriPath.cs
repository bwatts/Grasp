using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia
{
	public class RootUriPath : Behavior
	{
		UriPath _root;

		protected override void Given()
		{}

		protected override void When()
		{
			_root = UriPath.Root;
		}

		[Then]
		public void ValueIsEmpty()
		{
			Assert.That(_root.Value, Is.EqualTo(""));
		}

		[Then]
		public void BasePathIsNull()
		{
			Assert.That(_root.BasePath, Is.Null);
		}
	}
}