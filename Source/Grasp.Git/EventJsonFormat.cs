using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Json;
using Grasp.Messaging;
using Grasp.Persistence;
using Newtonsoft.Json;

namespace Grasp.Git
{
	public sealed class EventJsonFormat : Notion, IFormat<Event>
	{
		public static readonly Field<JsonConverter> _converterField = Field.On<EventJsonFormat>.For(x => x._converter);
		public static readonly Field<JsonSerializer> _serializerField = Field.On<EventJsonFormat>.For(x => x._serializer);
		public static readonly Field<Func<Stream, JsonTextWriter>> _writerFactoryField = Field.On<EventJsonFormat>.For(x => x._writerFactory);

		private JsonConverter _converter { get { return GetValue(_converterField); } set { SetValue(_converterField, value); } }
		private JsonSerializer _serializer { get { return GetValue(_serializerField); } set { SetValue(_serializerField, value); } }
		private Func<Stream, JsonTextWriter> _writerFactory { get { return GetValue(_writerFactoryField); } set { SetValue(_writerFactoryField, value); } }

		public EventJsonFormat(JsonConverter converter, JsonSerializer serializer, Func<Stream, JsonTextWriter> writerFactory)
		{
			Contract.Requires(converter != null);
			Contract.Requires(serializer != null);
			Contract.Requires(writerFactory != null);

			_converter = converter;
			_serializer = serializer;
			_writerFactory = writerFactory;
		}

		public Task WriteAsync(Event content, Stream stream)
		{
			return Task.Run(() =>
			{
				var writer = _writerFactory(stream);

				_converter.WriteJson(writer, content, _serializer);

				writer.Flush();
			});
		}

		public Task<Event> ReadAsync(Stream stream)
		{
			return Task.Run(() =>
			{
				var reader = new JsonTextReader(new StreamReader(stream));

				// TODO: Is it correct to only pass typeof(Event)?

				return (Event) _converter.ReadJson(reader, typeof(Event), null, _serializer);
			});
		}
	}
}