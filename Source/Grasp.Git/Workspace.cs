using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Grasp.Git
{
	public sealed class Workspace : Notion, IWorkspace
	{
		public static readonly Field<string> _folderPathField = Field.On<Workspace>.For(x => x._folderPath);
		public static readonly Field<IEventFormat> _eventFormatField = Field.On<Workspace>.For(x => x._eventFormat);
		public static readonly Field<Repository> _libGit2SharpRepositoryField = Field.On<Workspace>.For(x => x._libGit2SharpRepository);

		private string _folderPath { get { return GetValue(_folderPathField); } set { SetValue(_folderPathField, value); } }
		private IEventFormat _eventFormat { get { return GetValue(_eventFormatField); } set { SetValue(_eventFormatField, value); } }
		private Repository _libGit2SharpRepository { get { return GetValue(_libGit2SharpRepositoryField); } set { SetValue(_libGit2SharpRepositoryField, value); } }

		public Workspace(string folderPath, IEventFormat eventFormat, Repository libGit2SharpRepository)
		{
			Contract.Requires(folderPath != null);
			Contract.Requires(eventFormat != null);
			Contract.Requires(libGit2SharpRepository != null);

			_folderPath = folderPath;
			_eventFormat = eventFormat;
			_libGit2SharpRepository = libGit2SharpRepository;
		}

		public ITimelineFile OpenTimeline()
		{
			// TODO: What type of aggregate represents the global timeline?

			return new TimelineFile(GetFilePath("Timeline.topic"), _eventFormat, _libGit2SharpRepository);
		}

		public ITimelineFile OpenEnvironmentTimeline()
		{
			return new TimelineFile(GetFilePath("Environment.topic"), _eventFormat, _libGit2SharpRepository);
		}

		public ITimelineFile OpenAggregateTimeline(FullName name, Type type)
		{
			var aggregatePath = Path.Combine("Aggregates", name.ToString() + ".topic");

			return new TimelineFile(GetFilePath(aggregatePath), _eventFormat, _libGit2SharpRepository);
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