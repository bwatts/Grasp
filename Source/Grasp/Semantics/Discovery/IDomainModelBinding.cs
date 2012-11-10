using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery
{
	[ContractClass(typeof(IDomainModelBindingContract))]
	public interface IDomainModelBinding
	{
		DomainModel BindDomainModel();
	}

	[ContractClassFor(typeof(IDomainModelBinding))]
	internal abstract class IDomainModelBindingContract : IDomainModelBinding
	{
		DomainModel IDomainModelBinding.BindDomainModel()
		{
			Contract.Ensures(Contract.Result<DomainModel>() != null);

			return null;
		}
	}
}