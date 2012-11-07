using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;
using Slate.Web.Presentation.Lists;

namespace Slate.Web.Presentation.Home
{
	public class IndexListModel : ViewModel
	{
		public static readonly Field<Hyperlink> NewLinkField = Field.On<IndexListModel>.For(x => x.NewLink);
		public static readonly Field<HyperlistItem> ExploreItemField = Field.On<IndexListModel>.For(x => x.ExploreItem);
		public static readonly Field<ListModel> ListField = Field.On<IndexListModel>.For(x => x.List);

		public IndexListModel(Hyperlink newLink, HyperlistItem exploreItem, ListModel list)
		{
			Contract.Requires(newLink != null);
			Contract.Requires(exploreItem != null);
			Contract.Requires(list != null);

			NewLink = newLink;
			ExploreItem = exploreItem;
			List = list;
		}

		public Hyperlink NewLink { get { return GetValue(NewLinkField); } private set { SetValue(NewLinkField, value); } }
		public HyperlistItem ExploreItem { get { return GetValue(ExploreItemField); } private set { SetValue(ExploreItemField, value); } }
		public ListModel List { get { return GetValue(ListField); } private set { SetValue(ListField, value); } }
	}
}