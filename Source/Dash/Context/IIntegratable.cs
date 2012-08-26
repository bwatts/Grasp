using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Dash.Context
{
	[InheritedExport]
	[ContractClass(typeof(IIntegratableContract))]
	public interface IIntegratable
	{
		IHostable GetHostedSystem();
	}

	[ContractClassFor(typeof(IIntegratable))]
	internal abstract class IIntegratableContract : IIntegratable
	{
		IHostable IIntegratable.GetHostedSystem()
		{
			Contract.Ensures(Contract.Result<IHostable>() != null);

			return null;
		}
	}
}