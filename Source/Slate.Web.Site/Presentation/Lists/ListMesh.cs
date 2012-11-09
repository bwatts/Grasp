using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cloak.Http;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public sealed class ListMesh : Notion, IListMesh
	{
		public static readonly Field<Hyperlink> _countTemplateField = Field.On<ListMesh>.For(x => x._countTemplate);
		public static readonly Field<Hyperlink> _pageTemplateField = Field.On<ListMesh>.For(x => x._pageTemplate);
		public static readonly Field<Hyperlink> _itemTemplateField = Field.On<ListMesh>.For(x => x._itemTemplate);
		public static readonly Field<Func<HyperlistItem, object>> _itemIdSelectorField = Field.On<ListMesh>.For(x => x._itemIdSelector);

		private Hyperlink _countTemplate { get { return GetValue(_countTemplateField); } set { SetValue(_countTemplateField, value); } }
		private Hyperlink _pageTemplate { get { return GetValue(_pageTemplateField); } set { SetValue(_pageTemplateField, value); } }
		private Hyperlink _itemTemplate { get { return GetValue(_itemTemplateField); } set { SetValue(_itemTemplateField, value); } }
		private Func<HyperlistItem, object> _itemIdSelector { get { return GetValue(_itemIdSelectorField); } set { SetValue(_itemIdSelectorField, value); } }

		public ListMesh(Hyperlink countTemplate, Hyperlink pageTemplate, Hyperlink itemTemplate, Func<HyperlistItem, object> itemIdSelector)
		{
			Contract.Requires(countTemplate != null);
			Contract.Requires(pageTemplate != null);
			Contract.Requires(itemTemplate != null);
			Contract.Requires(itemIdSelector != null);

			_countTemplate = countTemplate;
			_pageTemplate = pageTemplate;
			_itemTemplate = itemTemplate;
			_itemIdSelector = itemIdSelector;
		}

		public Hyperlink GetCountLink(Hyperlist list)
		{
			return GetPageLink(list, list.Page.Number, _countTemplate);
		}

		public Hyperlink GetPageLink(Hyperlist list, Number number)
		{
			return GetPageLink(list, number, _pageTemplate);
		}

		public Hyperlink GetItemLink(Hyperlist list, HyperlistItem item)
		{
			var id = GetItemId(item);

			return _itemTemplate.BindVariables(new Dictionary<string, object>
			{
				{ "id", id },
				{ "id-escaped", Uri.EscapeDataString(id) },
				{ "item", item.ListItem.Number },
				{ "item-count", list.Context.ItemCount }
			});
		}

		private string GetItemId(HyperlistItem item)
		{
			var id = _itemIdSelector(item);

			return id is string ? (string) id : (id ?? "").ToString();
		}

		private static Hyperlink GetPageLink(Hyperlist list, Number number, Hyperlink template)
		{
			return template.BindVariables(new Dictionary<string, object>
			{
				{ "page", number },
				{ "page-count", list.Context.PageCount },
				{ "item", number },
				{ "item-count", list.Context.ItemCount }
			});
		}
	}
}