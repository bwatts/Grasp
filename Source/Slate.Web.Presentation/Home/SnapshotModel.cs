using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;

namespace Slate.Web.Presentation.Home
{
	public class SnapshotModel : ViewModel
	{
		public static readonly Field<Hyperlink> DashboardLinkField = Field.On<SnapshotModel>.For(x => x.DashboardLink);

		public SnapshotModel(Hyperlink dashboardLink)
		{
			Contract.Requires(dashboardLink != null);

			DashboardLink = dashboardLink;
		}

		public Hyperlink DashboardLink { get { return GetValue(DashboardLinkField); } private set { SetValue(DashboardLinkField, value); } }
	}
}