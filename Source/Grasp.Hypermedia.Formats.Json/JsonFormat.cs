using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using Grasp.Hypermedia.Http.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grasp.Hypermedia.Formats.Json
{
	public abstract class JsonFormat<TResource> : MediaFormat<TResource, JObject> where TResource : class
	{
		protected JsonFormat(MediaType mediaType) : base(mediaType)
		{}

		protected override JObject ReadMedia(Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
		{
			var json = ReadToken(stream) as JObject;

			if(json == null)
			{
				throw new FormatException(Resources.StreamNotJsonObject);
			}

			return json;
		}

		protected override void WriteMedia(JObject media, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
		{
			using(var writer = new StreamWriter(stream))
			using(var jsonWriter = new JsonTextWriter(writer))
			{
				media.WriteTo(jsonWriter);
			}
		}

		private static JToken ReadToken(Stream stream)
		{
			using(var reader = new StreamReader(stream))
			using(var jsonReader = new JsonTextReader(reader))
			{
				return JToken.ReadFrom(jsonReader);
			}
		}
	}
}