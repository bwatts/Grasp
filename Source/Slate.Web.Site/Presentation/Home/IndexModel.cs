using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;

namespace Slate.Web.Site.Presentation.Home
{
	public class IndexModel : ViewModel
	{
		public static readonly Field<SnapshotModel> SnapshotField = Field.On<IndexModel>.For(x => x.Snapshot);
		public static readonly Field<IndexListModel> FormListField = Field.On<IndexModel>.For(x => x.FormList);
		public static readonly Field<IndexListModel> IssueListField = Field.On<IndexModel>.For(x => x.IssueList);

		public IndexModel(SnapshotModel snapshot, IndexListModel formList, IndexListModel issueList)
		{
			Contract.Requires(snapshot != null);
			Contract.Requires(formList != null);
			Contract.Requires(issueList != null);

			Snapshot = snapshot;
			FormList = formList;
			IssueList = issueList;
		}

		public SnapshotModel Snapshot { get { return GetValue(SnapshotField); } private set { SetValue(SnapshotField, value); } }
		public IndexListModel FormList { get { return GetValue(FormListField); } private set { SetValue(FormListField, value); } }
		public IndexListModel IssueList { get { return GetValue(IssueListField); } private set { SetValue(IssueListField, value); } }
	}
}