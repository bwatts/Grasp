using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia;
using Grasp.Lists;

namespace Slate.Web.Presentation.Lists
{
	public static class ListPageLinks
	{
		public static Hyperlink BindListPageVariables(this Hyperlink link, ListPageKey pageKey, string pageVariable = "page", string pageSizeVariable = "page-size", string sortVariable = "sort")
		{
			Contract.Requires(link != null);
			Contract.Requires(pageKey != null);
			Contract.Requires(!String.IsNullOrEmpty(pageVariable));
			Contract.Requires(!String.IsNullOrEmpty(pageSizeVariable));
			Contract.Requires(!String.IsNullOrEmpty(sortVariable));

			return link.BindVariables(new Dictionary<string, object>
			{
				{ pageVariable, pageKey.Number },
				{ pageSizeVariable, pageKey.Size },
				{ sortVariable, pageKey.Sort }
			});
		}
	}
}