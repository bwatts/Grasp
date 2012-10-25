using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Slate.Http.Server.Configuration
{
	public class ServerSection : ConfigurationSection
	{
		[ConfigurationProperty("baseUrl", IsRequired = true)]
		public Uri BaseUrl
		{
			get { return (Uri) this["baseUrl"]; }
			set { this["baseUrl"] = value; }
		}
	}
}