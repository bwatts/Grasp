using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MRepresentation : Notion
	{
		public static MRepresentation Load(Stream xmlStream)
		{
			return Load(XElement.Load(xmlStream));
		}

		public static MRepresentation Load(XElement xml)
		{
			Contract.Requires(xml != null);

			return new MRepresentation(xml);
		}

		public static readonly Field<MHead> HeadField = Field.On<MRepresentation>.For(x => x.Head);
		public static readonly Field<MContent> BodyField = Field.On<MRepresentation>.For(x => x.Body);

		public MRepresentation(MHead head, MContent body = null)
		{
			Contract.Requires(head != null);

			Head = head;
			Body = body;
		}

		private MRepresentation(XElement xml)
		{
			// TODO: Framework which detects format errors and throws an exception that can be made into a 4xx response (should also apply in Load method)

			// See: Cloak.Xml

			throw new NotImplementedException();
		}

		public MHead Head { get { return GetValue(HeadField); } private set { SetValue(HeadField, value); } }
		public MContent Body { get { return GetValue(BodyField); } private set { SetValue(BodyField, value); } }

		public XDocument ToHtml()
		{
			var html = new XElement("html", Head.GetHtml());

			if(Body != null)
			{
				html.Add(Body.GetHtml());
			}

			return new XDocument(new XDocumentType("html", null, null, null), html);
		}

		public void Save(Stream stream)
		{
			ToHtml().Save(stream);
		}
	}
}