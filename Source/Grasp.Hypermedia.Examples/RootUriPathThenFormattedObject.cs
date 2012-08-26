using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia
{
	public class RootUriPathThenFormattedObject : Behavior
	{
		NumberFormatInfo _formatInfo;
		UriPath _result;

		protected override void Given()
		{
			_formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();

			_formatInfo.NegativeSign = "NEGATIVE";
		}

		protected override void When()
		{
			_result = UriPath.Root.Then(-1, _formatInfo);
		}

		[Then]
		public void BasePartIsRoot()
		{
			Assert.That(_result.BasePath, Is.EqualTo(UriPath.Root));
		}

		[Then]
		public void ValueIsFormattedObjectText()
		{
			Assert.That(_result.Value, Is.EqualTo("NEGATIVE1"));
		}
	}
}