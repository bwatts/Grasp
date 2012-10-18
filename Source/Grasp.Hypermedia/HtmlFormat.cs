using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Cloak.Http.Media;

namespace Grasp.Hypermedia
{
	public abstract class HtmlFormat<TMedia> : MediaFormat<TMedia, MRepresentation>
	{
		protected override void WriteRepresentation(MRepresentation representation, Stream stream, HttpContent content)
		{
			var html = representation.ToHtml();

			var settings = new XmlWriterSettings
			{
				Encoding = SelectCharacterEncoding(content.Headers),
				Indent = true,
				OmitXmlDeclaration = true,
				CloseOutput = false
			};

			using(var xmlWriter = XmlWriter.Create(stream, settings))
			{
				html.WriteTo(xmlWriter);
			}
		}

		protected override MRepresentation ReadRepresentation(Stream stream, HttpContent content, IFormatterLogger formatterLogger)
		{
			try
			{
				return MRepresentation.Load(stream);
			}
			catch(XmlException exception)
			{
				formatterLogger.LogError(exception.SourceUri, exception);

				throw new FormatException(Resources.StreamIsInvalidHtmlRepresentation, exception);
			}
		}
	}
}