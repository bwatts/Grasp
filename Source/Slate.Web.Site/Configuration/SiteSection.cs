using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Slate.Web.Site.Configuration
{
	public class SiteSection : ConfigurationSection
	{
		[ConfigurationProperty("apiBaseAddress", IsRequired = true)]
		public Uri ApiBaseAddress
		{
			get { return (Uri) this["apiBaseAddress"]; }
			set { this["apiBaseAddress"] = value; }
		}

		[ConfigurationProperty("formListUrl", IsRequired = true)]
		public Uri FormListUrl
		{
			get { return (Uri) this["formListUrl"]; }
			set { this["formListUrl"] = value; }
		}

		[ConfigurationProperty("issueListUrl", IsRequired = true)]
		public Uri IssueListUrl
		{
			get { return (Uri) this["issueListUrl"]; }
			set { this["issueListUrl"] = value; }
		}
	}
}