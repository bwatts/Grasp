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

namespace Slate.Web.Presentation.Lists
{
	public sealed class ListMesh : Notion, IListMesh
	{
		public static readonly Field<HtmlLink> _countTemplateField = Field.On<ListMesh>.For(x => x._countTemplate);
		public static readonly Field<HtmlLink> _pageNumberTemplateField = Field.On<ListMesh>.For(x => x._pageNumberTemplate);
		public static readonly Field<HtmlLink> _itemNumberTemplateField = Field.On<ListMesh>.For(x => x._itemNumberTemplate);
		public static readonly Field<HtmlLink> _itemTemplateField = Field.On<ListMesh>.For(x => x._itemTemplate);
		public static readonly Field<Func<object, string>> _itemIdSelectorField = Field.On<ListMesh>.For(x => x._itemIdSelector);

		private HtmlLink _countTemplate { get { return GetValue(_countTemplateField); } set { SetValue(_countTemplateField, value); } }
		private HtmlLink _pageNumberTemplate { get { return GetValue(_pageNumberTemplateField); } set { SetValue(_pageNumberTemplateField, value); } }
		private HtmlLink _itemNumberTemplate { get { return GetValue(_itemNumberTemplateField); } set { SetValue(_itemNumberTemplateField, value); } }
		private HtmlLink _itemTemplate { get { return GetValue(_itemTemplateField); } set { SetValue(_itemTemplateField, value); } }
		private Func<object, string> _itemIdSelector { get { return GetValue(_itemIdSelectorField); } set { SetValue(_itemIdSelectorField, value); } }

		public ListMesh(HtmlLink countTemplate, HtmlLink pageNumberTemplate, HtmlLink itemNumberTemplate, HtmlLink itemTemplate, Func<object, string> itemIdSelector)
		{
			Contract.Requires(countTemplate != null);
			Contract.Requires(pageNumberTemplate != null);
			Contract.Requires(itemNumberTemplate != null);
			Contract.Requires(itemTemplate != null);
			Contract.Requires(itemIdSelector != null);

			_countTemplate = countTemplate;
			_pageNumberTemplate = pageNumberTemplate;
			_itemNumberTemplate = itemNumberTemplate;
			_itemTemplate = itemTemplate;
			_itemIdSelector = itemIdSelector;
		}

		public HtmlLink GetCountLink(ListPage page)
		{
			return GetPagedLink(page, page.Key.Number, _countTemplate);
		}

		public HtmlLink GetPageLink(ListPage page, Number number)
		{
			return GetPagedLink(page, number, _pageNumberTemplate);
		}

		public HtmlLink GetItemLink(ListPage page, Number number)
		{
			return _itemNumberTemplate
				.BindParameter("item", number.ToString())
				.BindParameter("item-count", page.Context.ItemCount.ToString());
		}

		public HtmlLink GetItemLink(object id)
		{
			var text = _itemIdSelector(id);

			return _itemTemplate
				.BindParameter("id", text)
				.BindParameter("id-escaped", Href.Escape(text));
		}

		private static HtmlLink GetPagedLink(ListPage page, Number number, HtmlLink template)
		{
			return template
				.BindParameter("page", number.ToString())
				.BindParameter("page-count", page.Context.PageCount.ToString())
				.BindParameter("item-count", page.Context.ItemCount.ToString());
		}
	}
}