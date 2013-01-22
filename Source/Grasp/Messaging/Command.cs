using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message with the intent to initiate an action
	/// </summary>
	public abstract class Command : Message
	{
		public static readonly Field<FullName> WorkItemNameField = Field.On<Command>.For(x => x.WorkItemName);

		/// <summary>
		/// Initializes a command with the specified handle to a work item which tracks its progress
		/// </summary>
		/// <param name="workItemName">The unique hierarchical name of the work item tracking the command's progress</param>
		protected Command(FullName workItemName = default(FullName))
		{
			WorkItemName = workItemName ?? FullName.Anonymous;
		}

		/// <summary>
		/// Gets the unique hierarchical name of the work item tracking the command's progress
		/// </summary>
		public FullName WorkItemName { get { return GetValue(WorkItemNameField); } private set { SetValue(WorkItemNameField, value); } }
	}
}