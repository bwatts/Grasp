using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Hypermedia.Linq
{
	public class GetHtmlLinkWithRelationship : Behavior
	{
		string _elementName;
		Uri _href;
		string _relationship;
		HttpLink _link;
		XElement _htmlLink;

		protected override void Given()
		{
			_elementName = "link";

			_href = new Uri("http://localhost");

			_relationship = "self";

			_link = new HttpLink(_href, relationship: _relationship);
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
		public void HasRelationshipAttribute()
		{
			Assert.That(_htmlLink.Attribute("rel"), Is.Not.Null);
		}

		[Then]
		public void RelationshipIsOriginal()
		{
			Assert.That(_htmlLink.Attribute("rel").Value, Is.EqualTo(_relationship));
		}
	}
}