using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http.Media;

namespace Grasp.Hypermedia.Server
{
	public sealed class ExceptionCodeMap : IExceptionCodeMap
	{
		public HttpStatusCode GetStatusCode(Exception exception)
		{
			if(exception is UnauthorizedAccessException)
			{
				return HttpStatusCode.Unauthorized;
			}
			else if(exception is MediaFormatException)
			{
				return HttpStatusCode.BadRequest;
			}
			else
			{
				return HttpStatusCode.InternalServerError;
			}
		}

		public long GetErrorCode(Exception exception)
		{
			return 0;
		}
	}
}