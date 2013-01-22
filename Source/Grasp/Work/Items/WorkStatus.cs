using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Items
{
	/// <summary>
	/// The set of distinct states of a work item
	/// </summary>
	public enum WorkStatus
	{
		/// <summary>
		/// The work was accepted but has no progress
		/// </summary>
		Accepted,

		/// <summary>
		/// The work was accepted and has had one or more progress updates
		/// </summary>
		InProgress,

		/// <summary>
		/// The work finished successfully
		/// </summary>
		Finished,

		/// <summary>
		/// The work did not finish within the timeout interval
		/// </summary>
		TimedOut,

		/// <summary>
		/// The work encountered an issue preventing it from continuning
		/// </summary>
		Issue
	}
}