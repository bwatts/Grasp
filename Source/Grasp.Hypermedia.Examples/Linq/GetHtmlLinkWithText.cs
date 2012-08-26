using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Linq
{
	public class GetHtmlLinkWithText : Behavior
	{
		string _elementName;
		Uri _href;
		string _text;
		HttpLink _link;
		XElement _htmlLink;

		protected override void Given()
		{
			_elementName = "link";

			_href = new Uri("http://localhost");

			_text = "Text";

			_link = new HttpLink(_href, text: _text);
		}

		protected override void When()
		{
			_htmlLink = _link.ToHtmlLink(_elementName);
		}

		[Then]
		public void ElementNameIsOriginal()
		{
			Assert.That(_htmlLink.Name.ToString(), Is.EqualTo(_elementName));
		}

		[Then]
		public void HasHrefAttribute()
		{
			Assert.That(_htmlLink.Attribute("href"), Is.Not.Null);
		}

		[Then]
		public void HrefIsOriginal()
		{
			Assert.That(_htmlLink.Attribute("href").Value, Is.EqualTo(_href.ToString()));
		}

		[Then]
		public void HasTextValue()
		{
			Assert.That(_htmlLink.Value, Is.Not.Null);
		}

		[Then]
		public void TextIsOriginal()
		{
			Assert.That(_htmlLink.Value, Is.EqualTo(_text));
		}
	}
}