using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using LibGit2Sharp;

namespace Grasp.Git
{
	public sealed class Workspace : Notion, IWorkspace
	{
		public static readonly Field<string> _folderPathField = Field.On<Workspace>.For(x => x._folderPath);
		public static readonly Field<IFormat<EventFileBody>> _eventFileFormatField = Field.On<Workspace>.For(x => x._eventFileFormat);
		public static readonly Field<Repository> _libGit2SharpRepositoryField = Field.On<Workspace>.For(x => x._libGit2SharpRepository);

		private string _folderPath { get { return GetValue(_folderPathField); } set { SetValue(_folderPathField, value); } }
		private IFormat<EventFileBody> _eventFileFormat { get { return GetValue(_eventFileFormatField); } set { SetValue(_eventFileFormatField, value); } }
		private Repository _libGit2SharpRepository { get { return GetValue(_libGit2SharpRepositoryField); } set { SetValue(_libGit2SharpRepositoryField, value); } }

		public Workspace(string folderPath, IFormat<EventFileBody> eventFileFormat, Repository libGit2SharpRepository)
		{
			Contract.Requires(folderPath != null);
			Contract.Requires(eventFileFormat != null);
			Contract.Requires(libGit2SharpRepository != null);

			_folderPath = folderPath;
			_eventFileFormat = eventFileFormat;
			_libGit2SharpRepository = libGit2SharpRepository;
		}

		public ITimelineFile OpenTimeline()
		{
			return new TimelineFile(GetFilePath("Timeline.topic"), _eventFileFormat, _libGit2SharpRepository);
		}

		public ITimelineFile OpenEnvironmentTimeline()
		{
			return new TimelineFile(GetFilePath("Environment.topic"), _eventFileFormat, _libGit2SharpRepository);
		}

		public ITimelineFile OpenAggregateTimeline(FullName name, Type type)
		{
			var aggregatePath = Path.Combine("Aggregates", name.ToString() + ".topic");

			return new TimelineFile(GetFilePath(aggregatePath), _eventFileFormat, _libGit2SharpRepository);
		}

		public Task CommitToRepositoryAsync(string message)
		{
			_libGit2SharpRepository.Commit(message);

			return Task.Delay(0);
		}

		private string GetFilePath(string relativePath)
		{
			return Path.Combine(_folderPath, relativePath);
		}
	}
}