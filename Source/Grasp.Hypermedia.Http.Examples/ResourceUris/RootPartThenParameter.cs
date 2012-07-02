using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Media.ResourceUris
{
	public class RootPartThenParameter : Behavior
	{
		ResourceUriPart _result;

		protected override void Given()
		{}

		protected override void When()
		{
			_result = ResourceUriPart.Root.ThenParameter("p");
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(ResourceUriPart.Root));
		}

		[Then]
		public void ValueIsParameterParameterNameInBraces()
		{
			Assert.That(_result.Value, Is.EqualTo("{p}"));
		}
	}
}