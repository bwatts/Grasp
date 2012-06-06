using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Semantics.Discovery
{
	[ContractClass(typeof(IDomainModelSourceContract))]
	public interface IDomainModelSource
	{
		IEnumerable<DomainModelBinding> GetDomainModelBindings();
	}

	[ContractClassFor(typeof(IDomainModelSource))]
	internal abstract class IDomainModelSourceContract : IDomainModelSource
	{
		IEnumerable<DomainModelBinding> IDomainModelSource.GetDomainModelBindings()
		{
			Contract.Ensures(Contract.Result<IEnumerable<DomainModelBinding>>() != null);

			return null;
		}
	}
}