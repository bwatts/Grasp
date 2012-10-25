using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Slate.Web.Presentation.Lists;

namespace Slate.Web.Presentation.Home
{
	public sealed class IndexFactory : IIndexFactory
	{
		private readonly ISnapshotFactory _snapshotFactory;
		private readonly IListFactory _formListFactory;
		private readonly IListFactory _issueListFactory;

		public IndexFactory(ISnapshotFactory snapshotFactory, IListFactory formListFactory, IListFactory issuesListFactory)
		{
			Contract.Requires(snapshotFactory != null);
			Contract.Requires(formListFactory != null);
			Contract.Requires(issuesListFactory != null);

			_snapshotFactory = snapshotFactory;
			_formListFactory = formListFactory;
			_issueListFactory = issuesListFactory;
		}

		public async Task<IndexModel> CreateIndexAsync(ListPageKey formPageKey, ListPageKey issuePageKey)
		{
			var createSnapshotTask = _snapshotFactory.CreateSnapshotAsync();
			var createFormListTask = CreateFormListAsync(formPageKey);
			var createIssueListTask = CreateIssueListAsync(issuePageKey);

			await Task.WhenAll(createSnapshotTask, createFormListTask, createIssueListTask);

			return new IndexModel(createSnapshotTask.Result, createFormListTask.Result, createIssueListTask.Result);
		}

		private async Task<IndexListModel> CreateFormListAsync(ListPageKey formPageKey)
		{
			var exploreItem = new ListItem(Number.None, GetFormExploreValues());
			
			Mesh.LinkField.Set(exploreItem, new HtmlLink("explore/forms", "Explore forms..."));

			return new IndexListModel(
				newLink: new HtmlLink("explore/forms/start", "Start a form..."),
				exploreItem: exploreItem,
				list: await _formListFactory.CreateListAsync(formPageKey, item => item["Name"]));
		}

		private async Task<IndexListModel> CreateIssueListAsync(ListPageKey issuePageKey)
		{
			var exploreItem = new ListItem(Number.None, GetIssueExploreValues());

			Mesh.LinkField.Set(exploreItem, new HtmlLink("explore/issues", "Explore issues..."));

			return new IndexListModel(
				newLink: new HtmlLink("explore/issues/open", "Open an issue...", "Open an issue", new Relationship("grasp:open-issue")),
				exploreItem: exploreItem,
				list: await _issueListFactory.CreateListAsync(issuePageKey, item => item["Number"]));
		}

		private static IReadOnlyDictionary<string, object> GetFormExploreValues()
		{
			return AnonymousDictionary.Read(new
			{
				Total = 11,
				Drafts = 3,
				Published = 8,
				ResponseCount = 5,
				IssueCount = 2
			});
		}

		private static IReadOnlyDictionary<string, object> GetIssueExploreValues()
		{
			return AnonymousDictionary.Read(new
			{
				Total = 14,
				Calculations = 8,
				Variables = 4,
				Forms = 2
			});
		}
	}
}