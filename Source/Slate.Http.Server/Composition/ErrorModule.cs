using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Server;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition
{
	public class ErrorModule : BuilderModule
	{
		public ErrorModule(HttpConfiguration httpSettings, ServerSection serverSettings)
		{
			Contract.Requires(httpSettings != null);
			Contract.Requires(serverSettings != null);

			httpSettings.Filters.Add(new ApiExceptionFilterAttribute(
				new ApiErrorHtmlFormat(),
				new ExceptionCodeMap(),
				new HttpResourceContext(serverSettings.BaseUrl),
				serverSettings.IncludeStackTracesWithErrors));
		}
	}
}