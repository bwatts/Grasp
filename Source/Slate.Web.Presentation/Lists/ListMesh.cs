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

namespace Slate.Web.Presentation.Lists
{
	public sealed class ListMesh : Notion, IListMesh
	{
		public static readonly Field<Hyperlink> _countTemplateField = Field.On<ListMesh>.For(x => x._countTemplate);
		public static readonly Field<Hyperlink> _pageNumberTemplateField = Field.On<ListMesh>.For(x => x._pageNumberTemplate);
		public static readonly Field<Hyperlink> _itemNumberTemplateField = Field.On<ListMesh>.For(x => x._itemNumberTemplate);
		public static readonly Field<Hyperlink> _itemTemplateField = Field.On<ListMesh>.For(x => x._itemTemplate);
		public static readonly Field<Func<object, string>> _itemIdSelectorField = Field.On<ListMesh>.For(x => x._itemIdSelector);

		private Hyperlink _countTemplate { get { return GetValue(_countTemplateField); } set { SetValue(_countTemplateField, value); } }
		private Hyperlink _pageNumberTemplate { get { return GetValue(_pageNumberTemplateField); } set { SetValue(_pageNumberTemplateField, value); } }
		private Hyperlink _itemNumberTemplate { get { return GetValue(_itemNumberTemplateField); } set { SetValue(_itemNumberTemplateField, value); } }
		private Hyperlink _itemTemplate { get { return GetValue(_itemTemplateField); } set { SetValue(_itemTemplateField, value); } }
		private Func<object, string> _itemIdSelector { get { return GetValue(_itemIdSelectorField); } set { SetValue(_itemIdSelectorField, value); } }

		public ListMesh(Hyperlink countTemplate, Hyperlink pageNumberTemplate, Hyperlink itemNumberTemplate, Hyperlink itemTemplate, Func<object, string> itemIdSelector)
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

		public Hyperlink GetCountLink(Hyperlist list)
		{
			return GetPagedLink(list, list.Page.Number, _countTemplate);
		}

		public Hyperlink GetPageLink(Hyperlist list, Number number)
		{
			return GetPagedLink(list, number, _pageNumberTemplate);
		}

		public Hyperlink GetItemLink(Hyperlist list, Number number)
		{
			return _itemNumberTemplate.BindVariables(new Dictionary<string, string>
			{
				{ "item", number.ToString() },
				{ "item-count", list.Context.ItemCount.ToString() }
			});
		}

		public Hyperlink GetItemLink(object id)
		{
			var text = _itemIdSelector(id);

			return _itemNumberTemplate.BindVariables(new Dictionary<string, string>
			{
				{ "id", text },
				{ "id-escaped", Uri.EscapeDataString(text) }
			});
		}

		private Hyperlink GetPagedLink(Hyperlist list, Number number, Hyperlink template)
		{
			return _itemNumberTemplate.BindVariables(new Dictionary<string, string>
			{
				{ "page", number.ToString() },
				{ "page-count", list.Context.PageCount.ToString() },
				{ "item-count", list.Context.ItemCount.ToString() }
			});
		}
	}
}