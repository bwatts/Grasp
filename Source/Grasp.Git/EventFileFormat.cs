using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cloak;
using Grasp.Messaging;

namespace Grasp.Git
{
	public sealed class EventFileFormat : Notion, IFormat<EventFileBody>
	{
		public static readonly Field<IFormat<Event>> _eventFormatField = Field.On<EventFileFormat>.For(x => x._eventFormat);

		private IFormat<Event> _eventFormat { get { return GetValue(_eventFormatField); } set { SetValue(_eventFormatField, value); } }

		public EventFileFormat(IFormat<Event> eventFormat)
		{
			Contract.Requires(eventFormat != null);

			_eventFormat = eventFormat;
		}

		public async Task WriteAsync(EventFileBody content, Stream stream)
		{
			var writer = new StreamWriter(stream);

			var appending = content.Appending;

			foreach(var @event in content)
			{
				if(appending)
				{
					writer.WriteLine();
					writer.WriteLine();

					await writer.FlushAsync();
				}
				else
				{
					appending = true;
				}

				await _eventFormat.WriteAsync(@event, stream);
			}
		}

		public async Task<EventFileBody> ReadAsync(Stream stream)
		{
			return new EventFileBody(await ReadEventsAsync(stream));
		}

		private async Task<IEnumerable<Event>> ReadEventsAsync(Stream stream)
		{
			var events = new List<Event>();

			foreach(var eventStream in await ReadEventStreamsAsync(stream))
			{
				events.Add(await _eventFormat.ReadAsync(eventStream));
			}

			return events;
		}

		private static Task<IEnumerable<Stream>> ReadEventStreamsAsync(Stream stream)
		{
			return new EventStreamReadingContext(stream).ReadEventStreamsAsync();
		}

		private sealed class EventStreamReadingContext
		{
			private readonly Stream _stream;
			private readonly StreamReader _reader;
			private readonly List<Stream> _eventStreams = new List<Stream>();
			private MemoryStream _currentEvent;
			private StreamWriter _currentEventWriter;

			internal EventStreamReadingContext(Stream stream)
			{
				_stream = stream;

				_reader = new StreamReader(stream);
			}

			internal async Task<IEnumerable<Stream>> ReadEventStreamsAsync()
			{
				while(HasMoreEvents)
				{
					OnLineRead(await _reader.ReadLineAsync());
				}

				return _eventStreams;
			}

			private bool HasMoreEvents
			{
				get { return !_reader.EndOfStream; }
			}

			private void OnLineRead(string line)
			{
				AddCurrentEventText(line);

				if(String.IsNullOrEmpty(line) || !HasMoreEvents)
				{
					CommitCurrentEvent();
				}
				else
				{
					AddCurrentEventLine();
				}
			}

			private void CommitCurrentEvent()
			{
				if(_currentEvent != null)
				{
					_currentEventWriter.Flush();

					_currentEvent.Seek(0, SeekOrigin.Begin);

					_eventStreams.Add(_currentEvent);
				}

				_currentEvent = null;
				_currentEventWriter = null;
			}

			private void AddCurrentEventText(string text)
			{
				if(_currentEvent == null)
				{
					_currentEvent = new MemoryStream();

					_currentEventWriter = new StreamWriter(_currentEvent);
				}

				_currentEventWriter.Write(text);
			}

			private void AddCurrentEventLine()
			{
				AddCurrentEventText(Environment.NewLine);
			}
		}
	}
}