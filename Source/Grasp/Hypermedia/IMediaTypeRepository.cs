using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia
{
	[ContractClass(typeof(IMediaTypeRepositoryContract))]
	public interface IMediaTypeRepository
	{
		IEnumerable<MediaType> GetAll();

		MediaType GetByName(string name);
	}

	[ContractClassFor(typeof(IMediaTypeRepository))]
	internal abstract class IMediaTypeRepositoryContract : IMediaTypeRepository
	{
		IEnumerable<MediaType> IMediaTypeRepository.GetAll()
		{
			Contract.Ensures(Contract.Result<IEnumerable<MediaType>>() != null);

			return null;
		}

		MediaType IMediaTypeRepository.GetByName(string name)
		{
			Contract.Requires(name != null);
			Contract.Ensures(Contract.Result<MediaType>() != null);

			return null;
		}
	}
}