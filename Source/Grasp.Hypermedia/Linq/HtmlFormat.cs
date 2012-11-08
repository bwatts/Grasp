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

namespace Grasp.Hypermedia.Linq
{
	public abstract class HtmlFormat<TMedia> : MediaFormat<TMedia, MRepresentation>
	{
		protected HtmlFormat() : base()
		{
			IncludeMediaTypeLink = true;
		}

		protected HtmlFormat(MediaType mediaType) : base(mediaType)
		{
			IncludeMediaTypeLink = true;
		}

		protected HtmlFormat(params MediaType[] mediaTypes) : base(mediaTypes)
		{}

		public bool IncludeMediaTypeLink { get; set; }

		protected override void WriteRepresentation(MRepresentation representation, Stream stream, HttpContent content)
		{
			if(IncludeMediaTypeLink)
			{
				representation = AddMediaTypeLink(representation);
			}

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
			catch(XmlException xmlException)
			{
				formatterLogger.LogError("", xmlException);

				throw new FormatException(Resources.StreamIsInvalidHtmlRepresentation, xmlException);
			}
			catch(FormatException formatException)
			{
				if(formatterLogger != null)
				{
					formatterLogger.LogError("", formatException);
				}

				throw new FormatException(Resources.StreamIsInvalidHtmlRepresentation, formatException);
			}
		}

		private static MRepresentation AddMediaTypeLink(MRepresentation representation)
		{
			return new MRepresentation(AddMediaTypeLink(representation.Head), representation.Body);
		}

		private static MHead AddMediaTypeLink(MHead head)
		{
			return new MHead(head.Title, head.BaseLink, PrependMediaTypeLink(head));
		}

		private static IEnumerable<Hyperlink> PrependMediaTypeLink(MHead head)
		{
			// TODO: Make URL dynamic

			yield return new Hyperlink("media/application%2Fvnd.grasp.list%2Bhtml", relationship: new Relationship("grasp:media-type"));

			foreach(var link in head.Links)
			{
				yield return link;
			}
		}
	}
}