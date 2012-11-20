using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cloak;
using Cloak.Http;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public sealed class ListMesh : Notion, IListMesh
	{
		public static readonly Field<Hyperlink> _pageTemplateField = Field.On<ListMesh>.For(x => x._pageTemplate);
		public static readonly Field<Hyperlink> _itemCountTemplateField = Field.On<ListMesh>.For(x => x._itemCountTemplate);
		public static readonly Field<Hyperlink> _itemTemplateField = Field.On<ListMesh>.For(x => x._itemTemplate);
		public static readonly Field<Hyperlink> _itemNumberTemplateField = Field.On<ListMesh>.For(x => x._itemNumberTemplate);
		public static readonly Field<Func<HyperlistItem, object>> _itemIdSelectorField = Field.On<ListMesh>.For(x => x._itemIdSelector);

		private Hyperlink _pageTemplate { get { return GetValue(_pageTemplateField); } set { SetValue(_pageTemplateField, value); } }
		private Hyperlink _itemCountTemplate { get { return GetValue(_itemCountTemplateField); } set { SetValue(_itemCountTemplateField, value); } }
		private Hyperlink _itemTemplate { get { return GetValue(_itemTemplateField); } set { SetValue(_itemTemplateField, value); } }
		private Hyperlink _itemNumberTemplate { get { return GetValue(_itemNumberTemplateField); } set { SetValue(_itemNumberTemplateField, value); } }
		private Func<HyperlistItem, object> _itemIdSelector { get { return GetValue(_itemIdSelectorField); } set { SetValue(_itemIdSelectorField, value); } }

		public ListMesh(Hyperlink pageTemplate, Hyperlink itemCountTemplate, Hyperlink itemTemplate, Hyperlink itemNumberTemplate, Func<HyperlistItem, object> itemIdSelector)
		{
			Contract.Requires(pageTemplate != null);
			Contract.Requires(itemCountTemplate != null);
			Contract.Requires(itemTemplate != null);
			Contract.Requires(itemNumberTemplate != null);
			Contract.Requires(itemIdSelector != null);

			_pageTemplate = pageTemplate;
			_itemCountTemplate = itemCountTemplate;
			_itemTemplate = itemTemplate;
			_itemNumberTemplate = itemNumberTemplate;
			_itemIdSelector = itemIdSelector;
		}

		public Hyperlink GetPageLink(Hyperlist list, Count number)
		{
			return GetPageLink(list, number, _pageTemplate);
		}

		public Hyperlink GetItemCountLink(Hyperlist list)
		{
			return GetPageLink(list, new Count(list.Pages.Count.Value), _itemCountTemplate);
		}

		public Hyperlink GetItemLink(Hyperlist list, HyperlistItem item)
		{
			var id = GetItemId(item);

			return _itemTemplate.BindVariables(new Dictionary<string, object>
			{
				{ "id", id },
				{ "id-escaped", Uri.EscapeDataString(id) },
				{ "item", item.ListItem.Number },
				{ "total-items", list.Items.Total }
			});
		}

		public Hyperlink GetItemNumberLink(Hyperlist list, HyperlistItem item)
		{
			var id = GetItemId(item);

			return _itemNumberTemplate.BindVariables(new Dictionary<string, object>
			{
				{ "id", id },
				{ "id-escaped", Uri.EscapeDataString(id) },
				{ "item", item.ListItem.Number },
				{ "total-items", list.Items.Total }
			});
		}

		private string GetItemId(HyperlistItem item)
		{
			var id = _itemIdSelector(item);

			return id is string ? (string) id : (id ?? "").ToString();
		}

		private static Hyperlink GetPageLink(Hyperlist list, Count number, Hyperlink template)
		{
			return template.BindVariables(new Dictionary<string, object>
			{
				{ "page", number },
				{ "page-count", list.Pages.Count },
				{ "item", number },
				{ "total-items", list.Items.Total }
			});
		}
	}
}