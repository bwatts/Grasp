using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Slate.Web.Site.Configuration
{
	public class SiteSection : ConfigurationSection
	{
		[ConfigurationProperty("apiBaseUrl", IsRequired = true)]
		public Uri ApiBaseUrl
		{
			get { return (Uri) this["apiBaseUrl"]; }
			set { this["apiBaseUrl"] = value; }
		}
	}
}