using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;

namespace Grasp.Messaging
{
	/// <summary>
	/// Indicates the failure to apply a command
	/// </summary>
	public class CommandFailedEvent : Event
	{
		public static readonly Field<Command> CommandField = Field.On<CommandFailedEvent>.For(x => x.Command);
		public static readonly Field<Issue> IssueField = Field.On<CommandFailedEvent>.For(x => x.Issue);

		/// <summary>
		/// Initializes an event with the specified failed command
		/// </summary>
		/// <param name="command">The command which failed</param>
		/// <param name="issue">The issue which caused the failure</param>
		public CommandFailedEvent(Command command, Issue issue)
		{
			Contract.Requires(command != null);
			Contract.Requires(issue != null);

			Command = command;
			Issue = issue;
		}

		/// <summary>
		/// Gets the command which failed
		/// </summary>
		public Command Command { get { return GetValue(CommandField); } private set { SetValue(CommandField, value); } }

		/// <summary>
		/// Gets the issue which caused the failure
		/// </summary>
		public Issue Issue { get { return GetValue(IssueField); } private set { SetValue(IssueField, value); } }
	}
}