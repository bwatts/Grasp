using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Http.Mesh
{
	public class RootPartThenFormattedObject : Behavior
	{
		NumberFormatInfo _formatInfo;
		UriPart _result;

		protected override void Given()
		{
			_formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();

			_formatInfo.NegativeSign = "NEGATIVE";
		}

		protected override void When()
		{
			_result = UriPart.Root.Then(-1, _formatInfo);
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePart, Is.EqualTo(UriPart.Root));
		}

		[Then]
		public void ValueIsFormattedObjectText()
		{
			Assert.That(_result.Value, Is.EqualTo("NEGATIVE1"));
		}
	}
}