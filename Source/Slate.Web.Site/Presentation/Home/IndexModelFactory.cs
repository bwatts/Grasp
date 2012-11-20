using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;
using Slate.Web.Site.Presentation.Lists;

namespace Slate.Web.Site.Presentation.Home
{
	public sealed class IndexModelFactory : Notion, IIndexModelFactory
	{
		public static readonly Field<ISnapshotModelFactory> _snapshotModelFactoryField = Field.On<IndexModelFactory>.For(x => x._snapshotModelFactory);
		public static readonly Field<Uri> _formListUriField = Field.On<IndexModelFactory>.For(x => x._formListUri);
		public static readonly Field<Uri> _issueListUriField = Field.On<IndexModelFactory>.For(x => x._issueListUri);
		public static readonly Field<IListModelFactory> _formListFactoryField = Field.On<IndexModelFactory>.For(x => x._formListFactory);
		public static readonly Field<IListModelFactory> _issueListFactoryField = Field.On<IndexModelFactory>.For(x => x._issueListFactory);

		private ISnapshotModelFactory _snapshotModelFactory { get { return GetValue(_snapshotModelFactoryField); } set { SetValue(_snapshotModelFactoryField, value); } }
		private Uri _formListUri { get { return GetValue(_formListUriField); } set { SetValue(_formListUriField, value); } }
		private Uri _issueListUri { get { return GetValue(_issueListUriField); } set { SetValue(_issueListUriField, value); } }
		private IListModelFactory _formListFactory { get { return GetValue(_formListFactoryField); } set { SetValue(_formListFactoryField, value); } }
		private IListModelFactory _issueListFactory { get { return GetValue(_issueListFactoryField); } set { SetValue(_issueListFactoryField, value); } }

		public IndexModelFactory(ISnapshotModelFactory snapshotModelFactory, Uri formListUri, Uri issueListUri, IListModelFactory formListFactory, IListModelFactory issuesListFactory)
		{
			Contract.Requires(snapshotModelFactory != null);
			Contract.Requires(formListUri != null);
			Contract.Requires(issueListUri != null);
			Contract.Requires(formListFactory != null);
			Contract.Requires(issuesListFactory != null);

			_snapshotModelFactory = snapshotModelFactory;
			_formListUri = formListUri;
			_issueListUri = issueListUri;
			_formListFactory = formListFactory;
			_issueListFactory = issuesListFactory;
		}

		public async Task<IndexModel> CreateIndexModelAsync(ListViewKey formPageKey, ListViewKey issuePageKey)
		{
			var createSnapshotTask = _snapshotModelFactory.CreateSnapshotModelAsync();
			var createFormListTask = CreateFormListAsync(formPageKey);
			var createIssueListTask = CreateIssueListAsync(issuePageKey);

			await Task.WhenAll(createSnapshotTask, createFormListTask, createIssueListTask);

			return new IndexModel(createSnapshotTask.Result, createFormListTask.Result, createIssueListTask.Result);
		}

		private async Task<IndexListModel> CreateFormListAsync(ListViewKey formPageKey)
		{
			return new IndexListModel(
				newLink: new Hyperlink("explore/forms/start", "Start a form..."),
				exploreItem: new HyperlistItem(new Hyperlink("explore/forms", "Explore forms..."), new ListItem(Count.None, GetFormExploreValues())),
				list: await _formListFactory.CreateListModelAsync(_formListUri, formPageKey));
		}

		private async Task<IndexListModel> CreateIssueListAsync(ListViewKey issuePageKey)
		{
			return new IndexListModel(
				newLink: new Hyperlink("issues/open", "Open an issue...", "Open an issue"),
				exploreItem: new HyperlistItem(new Hyperlink("issues", "Manage issues..."), new ListItem(Count.None, GetIssueExploreValues())),
				list: await _issueListFactory.CreateListModelAsync(_issueListUri, issuePageKey));
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
				Forms = 2,
				Calculations = 8,
				Variables = 4
			});
		}
	}
}