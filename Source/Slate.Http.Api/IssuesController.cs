using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Http.Api
{
	public class IssuesController : ApiController
	{
		private readonly IIssueStore _issueStore;

		public IssuesController(IIssueStore issueStore)
		{
			Contract.Requires(issueStore != null);

			_issueStore = issueStore;
		}

		public Task<Hyperlist> GetListPageAsync(ListViewKey key)
		{
			return _issueStore.GetListAsync(new HyperlistQuery(
				new Hyperlink("issues", relationship: "grasp:list", @class: "issues"),
				key,
				"start",
				"size",
				"sort"));
		}
	}
}