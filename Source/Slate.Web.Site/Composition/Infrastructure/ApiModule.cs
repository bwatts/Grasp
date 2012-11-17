using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Autofac;
using Cloak.Autofac;
using Cloak.Http;
using Cloak.Http.Media;
using Grasp.Hypermedia;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition.Infrastructure
{
	public class ApiModule : BuilderModule
	{
		public ApiModule(HttpConfiguration httpSettings, SiteSection siteSettings)
		{
			Contract.Requires(httpSettings != null);
			Contract.Requires(siteSettings != null);

			var apiFormat = new ApiHtmlFormat();
			var errorFormat = new ApiErrorHtmlFormat();

			RegisterInstance(apiFormat).As<MediaFormat>();
			RegisterInstance(errorFormat).As<MediaFormat>();

			httpSettings.Formatters.Add(apiFormat);
			httpSettings.Formatters.Add(errorFormat);

			// There is an implicit contract between ApiClient and its factory for HttpClient. The container uses .ExternallyOwned to indicate
			// HttpClient instances are not tracked by the container for disposal. However, ApiClient has to make the assumption that it can
			// dispose instances safely; the delegate type Func<> does not signal the intent to dispose.
			//
			// The commonly accepted solution is a type which signals the intent to take full ownership. In Autofac, this is Owned<T>, which is a
			// great solution but requires an Autofac reference in each project using the technique. In the future, consider defining the same
			// concept in Cloak and Cloak.Autofac using this approach:
			//
			// http://stackoverflow.com/a/5446106/37815

			RegisterType<HttpClient>().InstancePerDependency().ExternallyOwned();

			// ApiSession fetches the definition of the API and caches it for the lifetime of the application. This neatly fulfills the
			// REST hypermedia constraint while allowing us to interact with entities and entity sets.

			Register(c => new ApiClient(siteSettings.ApiBaseAddress, c.Resolve<Func<HttpClient>>(), c.Resolve<MediaFormats>()))
			.SingleInstance();

			RegisterType<ApiSession>().As<IApiSession>().SingleInstance();
		}
	}
}