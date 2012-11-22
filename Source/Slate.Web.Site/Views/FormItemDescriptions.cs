using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web;
using DateTimeExtensions;
using Grasp.Hypermedia.Lists;
using Slate.Web.Site.Presentation.Lists;

namespace Slate.Web.Site.Views
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class FormItemDescriptions
	{
		public static string GetFormDescription(this ItemModel formItem)
		{
			Contract.Requires(formItem != null);

			var responseCount = formItem.Bindings.Read<int>("ResponseCount");
			var issueCount = formItem.Bindings.Read<int>("IssueCount");
			var whenCreated = formItem.Bindings.Read<DateTime>("WhenCreated");
			var whenModified = formItem.Bindings.Read<DateTime>("WhenModified");

			var relativeAge = whenCreated.ToNaturalText(whenModified);

			return new StringBuilder()
				.Append("[")
				.Append(formItem.Bindings.Read<string>("Phase").ToLower())
				.Append("] started ")
				.Append(relativeAge)
				.Append(" ago, ")
				.Append(responseCount)
				.Append(" ")
				.Append(responseCount == 1 ? "response" : "responses")
				.Append(", ")
				.Append(issueCount)
				.Append(" ")
				.Append(issueCount == 1 ? "issue" : "issues")
				.ToString();
		}

		public static string GetFormExploreDescription(this HyperlistItem formsExploreItem)
		{
			Contract.Requires(formsExploreItem != null);

			var total = formsExploreItem.ListItem.Bindings.Read<int>("Total");
			var drafts = formsExploreItem.ListItem.Bindings.Read<int>("Drafts");
			var published = formsExploreItem.ListItem.Bindings.Read<int>("Published");
			var responseCount = formsExploreItem.ListItem.Bindings.Read<int>("ResponseCount");
			var issueCount = formsExploreItem.ListItem.Bindings.Read<int>("IssueCount");

			return new StringBuilder()
				.Append(total)
				.Append(" total, ")
				.Append(drafts)
				.Append(" ")
				.Append(drafts == 1 ? "draft" : "drafts")
				.Append(", ")
				.Append(published)
				.Append(" published, ")
				.Append(responseCount)
				.Append(" ")
				.Append(responseCount == 1 ? "response" : "responses")
				.Append(" (")
				.Append(issueCount)
				.Append(" ")
				.Append(issueCount == 1 ? "issue" : "issues")
				.Append(")")
				.ToString();
		}
	}
}