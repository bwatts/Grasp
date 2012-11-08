using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Slate.Http.Server.Configuration
{
	public class SiteSection : ConfigurationSection
	{
		[ConfigurationProperty("baseUrl", IsRequired = true)]
		public Uri BaseUrl
		{
			get { return (Uri) this["baseUrl"]; }
			set { this["baseUrl"] = value; }
		}

		[ConfigurationProperty("connectionStringName", IsRequired = true)]
		public string ConnectionStringName
		{
			get { return (string) this["connectionStringName"]; }
			set { this["connectionStringName"] = value; }
		}
	}
}