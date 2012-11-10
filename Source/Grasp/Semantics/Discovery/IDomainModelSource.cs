using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery
{
	[ContractClass(typeof(IDomainModelSourceContract))]
	public interface IDomainModelSource
	{
		IEnumerable<IDomainModelBinding> BindDomainModels();
	}

	[ContractClassFor(typeof(IDomainModelSource))]
	internal abstract class IDomainModelSourceContract : IDomainModelSource
	{
		IEnumerable<IDomainModelBinding> IDomainModelSource.BindDomainModels()
		{
			Contract.Ensures(Contract.Result<IEnumerable<IDomainModelBinding>>() != null);

			return null;
		}
	}
}