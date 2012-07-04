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

namespace Grasp.Hypermedia.Http
{
	[ContractClass(typeof(MediaFormatContract<>))]
	public abstract class MediaFormat<TResource> : MediaTypeFormatter where TResource : class
	{
		protected MediaFormat(MediaType mediaType)
		{
			Contract.Requires(mediaType != null);

			SupportedMediaTypes.Add(mediaType.ToHeaderValue());
		}

		public override bool CanWriteType(Type type)
		{
			return type == typeof(TResource);
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			return Task.Factory.StartNew(() =>
			{
				return (object) ReadResource(stream, contentHeaders, formatterLogger);
			});
		}

		public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			return Task.Factory.StartNew(() =>
			{
				var resource = value as TResource;

				if(resource != null)
				{
					WriteResource(resource, stream, contentHeaders, transportContext);
				}
			});
		}

		protected abstract TResource ReadResource(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger);

		protected abstract void WriteResource(TResource resource, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext);

		protected void Supports(MediaType mediaType)
		{
			Contract.Requires(mediaType != null);

			SupportedMediaTypes.Add(mediaType.ToHeaderValue());
		}
	}

	[ContractClass(typeof(MediaFormatContract<,>))]
	public abstract class MediaFormat<TResource, TMedia> : MediaFormat<TResource> where TResource : class where TMedia : class
	{
		protected MediaFormat(MediaType mediaType) : base(mediaType)
		{}

		public override bool CanReadType(Type type)
		{
			return type == typeof(TMedia);
		}

		protected override TResource ReadResource(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			var media = ReadMedia(stream, contentHeaders, formatterLogger);

			return ConvertFromMedia(media);
		}

		protected override void WriteResource(TResource resource, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			var media = ConvertToMedia(resource);

			WriteMedia(media, stream, contentHeaders, transportContext);
		}

		protected abstract TResource ConvertFromMedia(TMedia media);

		protected abstract TMedia ConvertToMedia(TResource resource);

		protected abstract TMedia ReadMedia(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger);

		protected abstract void WriteMedia(TMedia media, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext);
	}

	[ContractClassFor(typeof(MediaFormat<>))]
	internal abstract class MediaFormatContract<TResource> : MediaFormat<TResource> where TResource : class
	{
		protected MediaFormatContract(MediaType mediaType) : base(mediaType)
		{}

		protected override TResource ReadResource(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			Contract.Requires(stream != null);
			Contract.Requires(contentHeaders != null);
			Contract.Requires(formatterLogger != null);

			return null;
		}

		protected override void WriteResource(TResource resource, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			Contract.Requires(resource != null);
			Contract.Requires(stream != null);
			Contract.Requires(contentHeaders != null);
			Contract.Requires(transportContext != null);
		}

		public override bool CanReadType(Type type)
		{
			return false;
		}
	}

	[ContractClassFor(typeof(MediaFormat<,>))]
	internal abstract class MediaFormatContract<TResource, TMedia> : MediaFormat<TResource, TMedia> where TResource : class where TMedia : class
	{
		protected MediaFormatContract(MediaType mediaType) : base(mediaType)
		{}

		protected override TResource ConvertFromMedia(TMedia media)
		{
			Contract.Requires(media != null);

			return null;
		}

		protected override TMedia ConvertToMedia(TResource resource)
		{
			Contract.Requires(resource != null);

			return null;
		}

		protected override TMedia ReadMedia(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			Contract.Requires(stream != null);
			Contract.Requires(contentHeaders != null);
			Contract.Requires(formatterLogger != null);

			return null;
		}

		protected override void WriteMedia(TMedia media, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			Contract.Requires(stream != null);
			Contract.Requires(contentHeaders != null);
			Contract.Requires(transportContext != null);
		}
	}
}