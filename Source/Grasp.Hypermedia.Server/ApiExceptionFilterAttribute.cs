using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Cloak.Http;
using Cloak.Http.Media;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia.Server
{
	// Though it is an attribute class, this is not intended to be used as an attribute, but rather added to the Filters collection of the
	// global Web API configuration. We have to derive from an attribute class to avoid reinventing some common handling that is only
	// available in the attribute base class (a curious choice).

	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly MediaFormat _errorFormat;
		private readonly IExceptionCodeMap _codeMap;
		private readonly IHttpResourceContext _resourceContext;
		private readonly bool _includeStackTraces;

		public ApiExceptionFilterAttribute(MediaFormat errorFormat, IExceptionCodeMap codeMap, IHttpResourceContext resourceContext, bool includeStackTraces)
		{
			Contract.Requires(errorFormat != null);
			Contract.Requires(codeMap != null);
			Contract.Requires(resourceContext != null);

			_errorFormat = errorFormat;
			_codeMap = codeMap;
			_resourceContext = resourceContext;
			_includeStackTraces = includeStackTraces;
		}

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			actionExecutedContext.Response = CreateErrorResponse(actionExecutedContext.Request, actionExecutedContext.Exception);
		}

		private HttpResponseMessage CreateErrorResponse(HttpRequestMessage request, Exception exception)
		{
			return new HttpResponseMessage(_codeMap.GetStatusCode(exception))
			{
				Content = new ObjectContent<ApiErrorResource>(CreateError(request, exception), _errorFormat)
			};
		}

		private ApiErrorResource CreateError(HttpRequestMessage request, Exception exception)
		{
			return new ApiErrorResource(GetHeader(request, exception), GetMessage(exception), _codeMap.GetErrorCode(exception));
		}

		private MHeader GetHeader(HttpRequestMessage request, Exception exception)
		{
			return _resourceContext.CreateHeader(Resources.ApiErrorCaption + exception.Message, request.RequestUri.PathAndQuery);
		}

		private string GetMessage(Exception exception)
		{
			return _includeStackTraces ? exception.ToString() : CleanStackTraces(exception);
		}

		private static string CleanStackTraces(Exception exception)
		{
			var message = new StringBuilder();

			while(exception != null)
			{
				if(message.Length > 0)
				{
					message.Append(" ---> ");
				}

				message.Append(exception.GetType()).Append(": ").Append(exception.Message);

				exception = exception.InnerException;
			}

			return message.ToString();
		}
	}
}