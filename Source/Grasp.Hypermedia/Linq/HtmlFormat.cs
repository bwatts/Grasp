using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Cloak;
using Cloak.Http.Media;

namespace Grasp.Hypermedia.Linq
{
	[ContractClass(typeof(HtmlFormatContract<>))]
	public abstract class HtmlFormat<TResource> : MediaFormat<TResource, MRepresentation> where TResource : HttpResource
	{
		public static readonly Func<MediaType, string> DefaultGetMediaTypeUrl = mediaType => "api/media/" + mediaType.Name;

		private Func<MediaType, string> _getMediaTypeUrl = DefaultGetMediaTypeUrl;

		protected HtmlFormat() : base(MediaType.HtmlTypes)
		{}

		protected HtmlFormat(IEnumerable<MediaType> supportedMediaTypes) : base(supportedMediaTypes.Concat(MediaType.HtmlTypes))
		{}

		protected HtmlFormat(params MediaType[] supportedMediaTypes) : this(supportedMediaTypes as IEnumerable<MediaType>)
		{}

		protected HtmlFormat(MediaType preferredMediaType) : base(preferredMediaType, MediaType.HtmlTypes)
		{}

		protected HtmlFormat(MediaType preferredMediaType, IEnumerable<MediaType> otherMediaTypes) : base(preferredMediaType, otherMediaTypes.Concat(MediaType.HtmlTypes))
		{}

		protected HtmlFormat(MediaType preferredMediaType, params MediaType[] otherMediaTypes) : this(preferredMediaType, otherMediaTypes as IEnumerable<MediaType>)
		{}

		public Func<MediaType, string> GetMediaTypeUrl
		{
			get { return _getMediaTypeUrl; }
			set
			{
				Contract.Requires(value != null);

				_getMediaTypeUrl = value;
			}
		}

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
			catch(XmlException xmlException)
			{
				if(formatterLogger != null)
				{
					formatterLogger.LogError("", xmlException);
				}

				if(stream.CanSeek)
				{
					stream.Seek(0, SeekOrigin.Begin);

					var xml = new StreamReader(stream).ReadToEnd();

					throw new FormatException(Resources.StreamIsInvalidXml.FormatInvariant(Environment.NewLine, xml), xmlException);
				}
				else
				{
					throw new FormatException(Resources.StreamIsInvalidHtmlRepresentation, xmlException);
				}
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

		protected override MRepresentation ConvertToRepresentation(TResource media)
		{
			return new MRepresentation(GetHeaderWithMediaTypeLink(media.Header), new MCompositeContent(ConvertFromResource(media)));
		}

		protected override TResource ConvertFromRepresentation(MRepresentation representation, IFormatterLogger formatterLogger)
		{
			return ConvertToResource(representation.Header, representation.Body);
		}

		protected abstract TResource ConvertToResource(MHeader header, MCompositeContent body);

		protected abstract IEnumerable<MContent> ConvertFromResource(TResource resource);

		protected abstract MClass MediaTypeClass { get; }

		private MHeader GetHeaderWithMediaTypeLink(MHeader header)
		{
			return new MHeader(header.Title, header.BaseLink, header.SelfLink, PrependPreferredMediaTypeLink(header));
		}

		private IEnumerable<Hyperlink> PrependPreferredMediaTypeLink(MHeader header)
		{
			if(HasPreferredMediaType)
			{
				var mediaTypeUrl = GetMediaTypeUrl(PreferredMediaType);

				if(mediaTypeUrl != null)
				{
					yield return new Hyperlink(mediaTypeUrl, relationship: "grasp:media-type", @class: MediaTypeClass);
				}
			}

			foreach(var link in header.Links)
			{
				yield return link;
			}
		}
	}

	[ContractClassFor(typeof(HtmlFormat<>))]
	internal abstract class HtmlFormatContract<TResource> : HtmlFormat<TResource> where TResource : HttpResource
	{
		protected override TResource ConvertToResource(MHeader header, MCompositeContent body)
		{
			Contract.Requires(header != null);
			Contract.Requires(body != null);
			Contract.Ensures(Contract.Result<TResource>() != null);

			return null;
		}

		protected override IEnumerable<MContent> ConvertFromResource(TResource resource)
		{
			Contract.Requires(resource != null);
			Contract.Ensures(Contract.Result<IEnumerable<MContent>>() != null);

			return null;
		}

		protected override MClass MediaTypeClass
		{
			get
			{
				Contract.Ensures(Contract.Result<MClass>() != null);

				return null;
			}
		}

		protected override MRepresentation ConvertToRepresentation(TResource media)
		{
 			return null;
		}

		protected override TResource ConvertFromRepresentation(MRepresentation representation, IFormatterLogger formatterLogger)
		{
 			return default(TResource);
		}
	}
}