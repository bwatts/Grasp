using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia.Server
{
	[ContractClass(typeof(IExceptionStatusCodeMapContract))]
	public interface IExceptionCodeMap
	{
		HttpStatusCode GetStatusCode(Exception exception);

		long GetErrorCode(Exception exception);
	}

	[ContractClassFor(typeof(IExceptionCodeMap))]
	internal abstract class IExceptionStatusCodeMapContract : IExceptionCodeMap
	{
		HttpStatusCode IExceptionCodeMap.GetStatusCode(Exception exception)
		{
			Contract.Requires(exception != null);

			return default(HttpStatusCode);
		}

		long IExceptionCodeMap.GetErrorCode(Exception exception)
		{
			Contract.Requires(exception != null);

			return 0;
		}
	}
}