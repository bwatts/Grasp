using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Dash.Infrastructure.Hosting;

namespace Dash.Infrastructure.Integration
{
	[InheritedExport]
	[ContractClass(typeof(IDashableContract))]
	public interface IDashable
	{
		IDashRuntime CreateRuntime();
	}

	[ContractClassFor(typeof(IDashable))]
	internal abstract class IDashableContract : IDashable
	{
		IDashRuntime IDashable.CreateRuntime()
		{
			Contract.Ensures(Contract.Result<IDashRuntime>() != null);

			return null;
		}
	}
}