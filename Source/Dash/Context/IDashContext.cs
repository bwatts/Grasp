using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Dash.Context
{
	[ContractClass(typeof(IDashContextContract))]
	public interface IDashContext
	{
		void HostSystem(IHostable system);
	}

	[ContractClassFor(typeof(IDashContext))]
	internal abstract class IDashContextContract : IDashContext
	{
		void IDashContext.HostSystem(IHostable system)
		{
			Contract.Requires(system != null);
		}
	}
}