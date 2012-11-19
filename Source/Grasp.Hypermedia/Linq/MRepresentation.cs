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
		public static MRepresentation Load(XDocument xml)
		{
			Contract.Requires(xml != null);

			return MRepresentationReader.Read(xml);
		}

		public static MRepresentation Load(Stream xmlStream)
		{
			return Load(XDocument.Load(xmlStream));
		}

		public static readonly Field<MHeader> HeaderField = Field.On<MRepresentation>.For(x => x.Header);
		public static readonly Field<MCompositeContent> BodyField = Field.On<MRepresentation>.For(x => x.Body);

		public MRepresentation(MHeader header, MCompositeContent body)
		{
			Contract.Requires(header != null);
			Contract.Requires(body != null);

			Header = header;
			Body = body;
		}

		public MHeader Header { get { return GetValue(HeaderField); } private set { SetValue(HeaderField, value); } }
		public MCompositeContent Body { get { return GetValue(BodyField); } private set { SetValue(BodyField, value); } }

		public XDocument ToHtml()
		{
			return new XDocument(new XElement(
				"html",
				Header.GetHtml(),
				new XElement("body", Body.GetHtml())));
		}

		public void Save(Stream stream)
		{
			ToHtml().Save(stream);
		}
	}
}