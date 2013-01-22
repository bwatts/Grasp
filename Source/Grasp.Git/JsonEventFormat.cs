using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Json;
using Grasp.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grasp.Git
{
	public sealed class JsonEventFormat : Notion, IEventFormat
	{
		public static readonly Field<JsonConverter> _jsonConverterField = Field.On<JsonEventFormat>.For(x => x._jsonConverter);

		private JsonConverter _jsonConverter { get { return GetValue(_jsonConverterField); } set { SetValue(_jsonConverterField, value); } }

		public JsonEventFormat(JsonConverter jsonConverter)
		{
			Contract.Requires(jsonConverter != null);

			_jsonConverter = jsonConverter;
		}

		public Task WriteEventsAsync(Stream stream, bool hasEvents, IEnumerable<Event> events)
		{
			var writer = new StreamWriter(stream);
			var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented };

			foreach(var @event in events)
			{
				if(hasEvents)
				{
					writer.WriteLine();
					writer.WriteLine();
				}
				else
				{
					hasEvents = true;
				}

				// TODO: Externalize serializer creation

				_jsonConverter.WriteJson(jsonWriter, @event, new JsonSerializer { Formatting = Formatting.Indented });
			}

			writer.Flush();

			return Task.Delay(0);
		}

		public async Task<IEnumerable<Event>> ReadEventsAsync(Stream stream)
		{
			// TODO: Better implementation and more robust error reporting

			var reader = new StreamReader(stream);

			var eventJson = new StringBuilder();
			var events = new List<Event>();

			while(!reader.EndOfStream)
			{
				var line = await reader.ReadLineAsync();

				if(String.IsNullOrEmpty(line))
				{
					events.Add(ReadEvent(eventJson.ToString()));

					eventJson = new StringBuilder();
				}
				else
				{
					eventJson.AppendLine(line);
				}
			}

			if(eventJson.Length > 0)
			{
				events.Add(ReadEvent(eventJson.ToString()));
			}

			return events;
		}

		private Event ReadEvent(string json)
		{
			// TODO: Externalize serializer creation

			// TODO: Is it correct to only pass typeof(Event) here?

			return (Event) _jsonConverter.ReadJson(
				new JsonTextReader(new StringReader(json)),
				typeof(Event),
				null,
				new JsonSerializer { Formatting = Formatting.Indented });
		}
	}
}