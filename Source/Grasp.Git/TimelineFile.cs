using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Messaging;
using LibGit2Sharp;

namespace Grasp.Git
{
	public sealed class TimelineFile : Notion, ITimelineFile
	{
		public static readonly Field<string> _pathField = Field.On<TimelineFile>.For(x => x._path);
		public static readonly Field<IFormat<EventFileBody>> _formatField = Field.On<TimelineFile>.For(x => x._format);
		public static readonly Field<Repository> _libGit2SharpRepositoryField = Field.On<TimelineFile>.For(x => x._libGit2SharpRepository);

		private string _path { get { return GetValue(_pathField); } set { SetValue(_pathField, value); } }
		private IFormat<EventFileBody> _format { get { return GetValue(_formatField); } set { SetValue(_formatField, value); } }
		private Repository _libGit2SharpRepository { get { return GetValue(_libGit2SharpRepositoryField); } set { SetValue(_libGit2SharpRepositoryField, value); } }

		public TimelineFile(string path, IFormat<EventFileBody> format, Repository libGit2SharpRepository)
		{
			Contract.Requires(path != null);
			Contract.Requires(format != null);
			Contract.Requires(libGit2SharpRepository != null);

			_path = path;
			_format = format;
			_libGit2SharpRepository = libGit2SharpRepository;
		}

		public async Task AppendEventsAsync(IEnumerable<Event> events)
		{
			await WriteEventsAsync(events);

			_libGit2SharpRepository.Index.Stage(_path);
		}

		public async Task<IEnumerable<Event>> ReadEventsAsync()
		{
			using(var stream = File.OpenRead(_path))
			{

				var events = await _format.ReadAsync(stream);

				return events.ToList();
			}
		}

		private async Task WriteEventsAsync(IEnumerable<Event> events)
		{
			using(var fileStream = File.Open(_path, FileMode.Append))
			{
				await WriteBodyAsync(events, fileStream);
			}
		}

		private Task WriteBodyAsync(IEnumerable<Event> events, Stream fileStream)
		{
			return _format.WriteAsync(new EventFileBody(events, FileAlreadyExisted), fileStream);
		}

		private static bool FileAlreadyExisted
		{
			get { return new Win32Exception().NativeErrorCode == 183; }
		}
	}
}