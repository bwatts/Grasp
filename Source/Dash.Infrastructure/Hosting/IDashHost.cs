using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Dash.Infrastructure.Hosting
{
	[ContractClass(typeof(IDashHostContract))]
	public interface IDashHost
	{
		void Execute(IDashRuntime runtime);
	}

	[ContractClassFor(typeof(IDashHost))]
	internal abstract class IDashHostContract : IDashHost
	{
		void IDashHost.Execute(IDashRuntime runtime)
		{
			Contract.Requires(runtime != null);
		}
	}
}