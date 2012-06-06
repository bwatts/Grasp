using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia
{
	[ContractClass(typeof(IRelationshipRepositoryContract))]
	public interface IRelationshipRepository
	{
		IEnumerable<Relationship> GetAll();

		Relationship GetByName(string name);
	}

	[ContractClassFor(typeof(IRelationshipRepository))]
	internal abstract class IRelationshipRepositoryContract : IRelationshipRepository
	{
		IEnumerable<Relationship> IRelationshipRepository.GetAll()
		{
			Contract.Ensures(Contract.Result<IEnumerable<Relationship>>() != null);

			return null;
		}

		Relationship IRelationshipRepository.GetByName(string name)
		{
			Contract.Requires(name != null);
			Contract.Ensures(Contract.Result<Relationship>() != null);

			return null;
		}
	}
}