using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Grasp.Hypermedia.Http.Media;

namespace Grasp.Hypermedia.Formats.Xthml
{
	public abstract class XhtmlFormat<TResource> : MediaFormat<TResource, XElement> where TResource : class
	{
		protected XhtmlFormat(MediaType mediaType) : base(mediaType)
		{}

		protected override XElement ReadMedia(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			var element = ReadNode(stream) as XElement;

			if(element == null)
			{
				throw new FormatException(Resources.StreamNotXmlElement);
			}

			return element;
		}

		protected override void WriteMedia(XElement media, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			using(var writer = new StreamWriter(stream))
			using(var xmlWriter = new XmlTextWriter(writer))
			{
				media.WriteTo(xmlWriter);
			}
		}

		private static XNode ReadNode(Stream stream)
		{
			using(var reader = new StreamReader(stream))
			using(var xmlReader = new XmlTextReader(reader))
			{
				xmlReader.MoveToContent();

				return XNode.ReadFrom(xmlReader);
			}
		}
	}
}