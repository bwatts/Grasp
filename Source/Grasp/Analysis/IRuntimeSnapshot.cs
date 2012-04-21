using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Analysis
{
	/// <summary>
	/// Describes the state of a <see cref="GraspRuntime"/> at a particular point in time
	/// </summary>
	[ContractClass(typeof(IRuntimeSnapshotContract))]
	public interface IRuntimeSnapshot
	{
		/// <summary>
		/// Gets the value of the specified variable
		/// </summary>
		/// <param name="variable">The variable for which to retrieve the value</param>
		/// <returns>The value associated with the variable</returns>
		object GetValue(Variable variable);
	}

	[ContractClassFor(typeof(IRuntimeSnapshot))]
	internal abstract class IRuntimeSnapshotContract : IRuntimeSnapshot
	{
		object IRuntimeSnapshot.GetValue(Variable variable)
		{
			Contract.Requires(variable != null);

			return null;
		}
	}
}