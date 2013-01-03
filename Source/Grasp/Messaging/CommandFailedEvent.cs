using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// Indicates the failure to apply a command
	/// </summary>
	public class CommandFailedEvent : Event
	{
		public static readonly Field<Command> CommandField = Field.On<CommandFailedEvent>.For(x => x.Command);
		public static readonly Field<FullName> CauseNameField = Field.On<CommandFailedEvent>.For(x => x.CauseName);
		public static readonly Field<string> CauseDescriptionField = Field.On<CommandFailedEvent>.For(x => x.CauseDescription);

		/// <summary>
		/// Initializes an event with the specified failed command
		/// </summary>
		/// <param name="workItemId">The identifier of the work item tracking the command's progress</param>
		/// <param name="causeName">The unique name of the cause of failure</param>
		/// <param name="causeDescription">A description of the specific failure</param>
		public CommandFailedEvent(Command command, FullName causeName, string causeDescription)
		{
			Contract.Requires(command != null);
			Contract.Requires(causeName != null);
			Contract.Requires(causeDescription != null);

			Command = command;
			CauseName = causeName;
			CauseDescription = causeDescription;
		}

		/// <summary>
		/// Gets the command which failed
		/// </summary>
		public Command Command { get { return GetValue(CommandField); } private set { SetValue(CommandField, value); } }

		/// <summary>
		/// Gets the unique name of the cause of failure
		/// </summary>
		public FullName CauseName { get { return GetValue(CauseNameField); } private set { SetValue(CauseNameField, value); } }

		/// <summary>
		/// Gets the description of the specific cause of failure
		/// </summary>
		public string CauseDescription { get { return GetValue(CauseDescriptionField); } private set { SetValue(CauseDescriptionField, value); } }
	}
}