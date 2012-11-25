using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	/// <summary>
	/// Describes the state of a <see cref="SchemaBinding"/> at a particular point in time
	/// </summary>
	[ContractClass(typeof(IRuntimeSnapshotContract))]
	public interface ISnapshot
	{
		/// <summary>
		/// Gets the value of the specified variable
		/// </summary>
		/// <param name="name">The variable for which to retrieve the value</param>
		/// <returns>The value associated with the variable</returns>
		object GetValue(FullName name);
	}

	[ContractClassFor(typeof(ISnapshot))]
	internal abstract class IRuntimeSnapshotContract : ISnapshot
	{
		object ISnapshot.GetValue(FullName name)
		{
			Contract.Requires(name != null);

			return null;
		}
	}
}