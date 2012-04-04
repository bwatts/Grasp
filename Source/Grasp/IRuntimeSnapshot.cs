using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	[ContractClass(typeof(IRuntimeSnapshotContract))]
	public interface IRuntimeSnapshot
	{
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