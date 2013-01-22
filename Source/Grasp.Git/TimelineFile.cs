using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using LibGit2Sharp;

namespace Grasp.Git
{
	public sealed class TimelineFile : Notion, ITimelineFile
	{
		public static readonly Field<string> _pathField = Field.On<TimelineFile>.For(x => x._path);
		public static readonly Field<IEventFormat> _eventFormatField = Field.On<TimelineFile>.For(x => x._eventFormat);
		public static readonly Field<Repository> _libGit2SharpRepositoryField = Field.On<TimelineFile>.For(x => x._libGit2SharpRepository);

		private string _path { get { return GetValue(_pathField); } set { SetValue(_pathField, value); } }
		private IEventFormat _eventFormat { get { return GetValue(_eventFormatField); } set { SetValue(_eventFormatField, value); } }
		private Repository _libGit2SharpRepository { get { return GetValue(_libGit2SharpRepositoryField); } set { SetValue(_libGit2SharpRepositoryField, value); } }

		public TimelineFile(string path, IEventFormat eventFormat, Repository libGit2SharpRepository)
		{
			Contract.Requires(path != null);
			Contract.Requires(eventFormat != null);
			Contract.Requires(libGit2SharpRepository != null);

			_path = path;
			_eventFormat = eventFormat;
			_libGit2SharpRepository = libGit2SharpRepository;
		}

		public async Task AppendEventsAsync(IEnumerable<Event> events)
		{
			await WriteEventsAsync(events);

			_libGit2SharpRepository.Index.Stage(_path);
		}

		public async Task<List<Event>> ReadEventsAsync()
		{
			using(var fileStream = File.OpenRead(_path))
			{
				var events = await _eventFormat.ReadEventsAsync(fileStream);

				return events.ToList();
			}
		}

		private async Task WriteEventsAsync(IEnumerable<Event> events)
		{
			using(var fileStream = File.Open(_path, FileMode.Append))
			using(var writer = new StreamWriter(fileStream))
			{
				await _eventFormat.WriteEventsAsync(fileStream, FileAlreadyExisted, events);

				await writer.FlushAsync();
			}
		}

		private static bool FileAlreadyExisted
		{
			get { return new Win32Exception().NativeErrorCode == 183; }
		}
	}
}