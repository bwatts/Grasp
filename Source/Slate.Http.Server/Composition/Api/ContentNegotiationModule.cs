using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Cloak.Autofac;

namespace Slate.Http.Server.Composition.Api
{
	public class ContentNegotiationModule : BuilderModule
	{
		public ContentNegotiationModule(HttpConfiguration httpSettings)
		{
			Contract.Requires(httpSettings != null);

			// Respond with a 406 instead of application/json if content negotiation fails

			httpSettings.Services.Replace(typeof(IContentNegotiator), new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));
		}
	}
}