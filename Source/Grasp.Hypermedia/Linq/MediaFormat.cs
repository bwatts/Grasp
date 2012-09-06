using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia.Linq
{
	[ContractClass(typeof(MediaFormatContract<>))]
	public abstract class MediaFormat<TMedia> : MediaTypeFormatter where TMedia : class
	{
		protected MediaFormat()
		{
			SetDefaultSupportedMediaTypes();
		}

		protected MediaFormat(MediaType mediaType)
		{
			SetDefaultSupportedMediaTypes();

			Supports(mediaType);
		}

		protected MediaFormat(params MediaType[] mediaTypes)
		{
			SetDefaultSupportedMediaTypes();

			Supports(mediaTypes);
		}

		public override bool CanReadType(Type type)
		{
			return type == typeof(TMedia);
		}

		public override bool CanWriteType(Type type)
		{
			return type == typeof(TMedia);
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			return Task.Factory.StartNew(() =>
			{
				return (object) ReadMedia(stream, contentHeaders, formatterLogger);
			});
		}

		public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			return Task.Factory.StartNew(() =>
			{
				var media = value as TMedia;

				if(media != null)
				{
					WriteMedia(media, stream, contentHeaders, transportContext);
				}
			});
		}

		protected virtual TMedia ReadMedia(Stream xmlStream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			var representation = ReadRepresentation(xmlStream, contentHeaders, formatterLogger);

			return ConvertFromRepresentation(representation);
		}

		protected virtual void WriteMedia(TMedia media, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			WriteRepresentation(ConvertToRepresentation(media), stream, contentHeaders, transportContext);
		}

		protected virtual MRepresentation ReadRepresentation(Stream xmlStream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			return MRepresentation.Load(xmlStream);
		}

		protected virtual void WriteRepresentation(MRepresentation representation, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			Contract.Requires(representation != null);

			representation.Save(stream);
		}

		protected abstract TMedia ConvertFromRepresentation(MRepresentation representation);

		protected abstract MRepresentation ConvertToRepresentation(TMedia media);

		private void SetDefaultSupportedMediaTypes()
		{
			SupportedMediaTypes.Clear();

			Supports(MediaType.XmlTypes);
		}

		protected void Supports(MediaType mediaType)
		{
			Contract.Requires(mediaType != null);

			SupportedMediaTypes.Add(mediaType.ToHeaderValue());
		}

		protected void Supports(IEnumerable<MediaType> mediaTypes)
		{
			Contract.Requires(mediaTypes != null);

			foreach(var mediaType in mediaTypes)
			{
				Supports(mediaType);
			}
		}

		protected void Supports(params MediaType[] mediaTypes)
		{
			Supports(mediaTypes as IEnumerable<MediaType>);
		}
	}

	[ContractClassFor(typeof(MediaFormat<>))]
	internal abstract class MediaFormatContract<TMedia> : MediaFormat<TMedia> where TMedia : class
	{
		protected override TMedia ConvertFromRepresentation(MRepresentation representation)
		{
			Contract.Requires(representation != null);

			return null;
		}

		protected override MRepresentation ConvertToRepresentation(TMedia media)
		{
			Contract.Requires(media != null);
			Contract.Ensures(Contract.Result<MRepresentation>() != null);

			return null;
		}
	}
}