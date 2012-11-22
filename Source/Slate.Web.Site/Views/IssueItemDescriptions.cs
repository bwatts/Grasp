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
	public static class IssueItemDescriptions
	{
		public static string GetIssueDescription(this ItemModel formItem)
		{
			Contract.Requires(formItem != null);

			var assignedTo = formItem.Bindings.Read<string>("AssignedTo");
			var whenCreated = formItem.Bindings.Read<DateTime>("WhenCreated");
			var whenModified = formItem.Bindings.Read<DateTime>("WhenModified");

			var relativeAge = whenCreated.ToNaturalText(whenModified);

			return new StringBuilder()
				.Append("created ")
				.Append(relativeAge)
				.Append(" ago, assigned to ")
				.Append(assignedTo)
				.ToString();
		}

		public static string GetIssueExploreDescription(this HyperlistItem issuesExploreItem)
		{
			Contract.Requires(issuesExploreItem != null);

			var total = issuesExploreItem.ListItem.Bindings.Read<int>("Total");
			var forms = issuesExploreItem.ListItem.Bindings.Read<int>("Forms");
			var calculations = issuesExploreItem.ListItem.Bindings.Read<int>("Calculations");
			var variables = issuesExploreItem.ListItem.Bindings.Read<int>("Variables");

			return new StringBuilder()
				.Append(total)
				.Append(" total, ")
				.Append(forms)
				.Append(" ")
				.Append(forms == 1 ? "form" : "forms")
				.Append(", ")
				.Append(calculations)
				.Append(" ")
				.Append(calculations == 1 ? "calculation" : "calculations")
				.Append(", ")
				.Append(variables)
				.Append(" ")
				.Append(variables == 1 ? "variable" : "variables")
				.ToString();
		}
	}
}