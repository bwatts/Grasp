using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Slate.Web.Presentation.Lists;

namespace Slate.Web.Presentation.Home
{
	public class IndexListModel : ViewModel
	{
		public static readonly Field<HtmlLink> NewLinkField = Field.On<IndexListModel>.For(x => x.NewLink);
		public static readonly Field<ListItem> ExploreItemField = Field.On<IndexListModel>.For(x => x.ExploreItem);
		public static readonly Field<ListModel> ListField = Field.On<IndexListModel>.For(x => x.List);

		public IndexListModel(HtmlLink newLink, ListItem exploreItem, ListModel list)
		{
			Contract.Requires(newLink != null);
			Contract.Requires(exploreItem != null);
			Contract.Requires(list != null);

			NewLink = newLink;
			ExploreItem = exploreItem;
			List = list;
		}

		public HtmlLink NewLink { get { return GetValue(NewLinkField); } private set { SetValue(NewLinkField, value); } }
		public ListItem ExploreItem { get { return GetValue(ExploreItemField); } private set { SetValue(ExploreItemField, value); } }
		public ListModel List { get { return GetValue(ListField); } private set { SetValue(ListField, value); } }
	}
}