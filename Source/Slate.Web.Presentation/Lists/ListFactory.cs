using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;

namespace Slate.Web.Presentation.Lists
{
	public sealed class ListFactory : Notion, IListFactory
	{
		public static readonly Field<IListClient> _clientField = Field.On<ListFactory>.For(x => x._client);
		public static readonly Field<string> _emptyMessageField = Field.On<ListFactory>.For(x => x._emptyMessage);

		private IListClient _client { get { return GetValue(_clientField); } set { SetValue(_clientField, value); } }
		private string _emptyMessage { get { return GetValue(_emptyMessageField); } set { SetValue(_emptyMessageField, value); } }

		public ListFactory(IListClient client, string emptyMessage)
		{
			Contract.Requires(client != null);
			Contract.Requires(emptyMessage != null);

			_client = client;
			_emptyMessage = emptyMessage;
		}

		public async Task<ListModel> CreateListAsync(ListPageKey pageKey, Func<ListItem, object> itemIdSelector)
		{
			var list = await _client.GetListAsync(pageKey);

			var pageHrefTemplate = Mesh.LinkField.Get(list);

			var numbers = Enumerable
				.Range(1, list.Context.PageCount.Value)
				.Select(number => new Number(number))
				.Select(number => new NumberModel(number, pageHrefTemplate.BindParameter("page", number.ToString())))
				.ToList();

			//return numbers.Count == 0 ? CreateEmptyListModel(list) : CreateListModel(list, numbers, itemIdSelector);
			return null;
		}

		//private ListModel CreateEmptyListModel(ListPage page)
		//{
		//	return new ListModel(new ContextModel(_mesh.GetCountLink(page)), _emptyMessage);
		//}

		//private ListModel CreateListModel(ListPage page, List<NumberModel> numbers, Func<ListItem, object> itemIdSelector)
		//{
		//	return new ListModel(
		//		new ContextModel(
		//			itemCount: new NumberModel(page.Context.ItemCount, _mesh.GetCountLink(page)),
		//			first: numbers.First(),
		//			last: numbers.Last(),
		//			previous: numbers.ElementAt(page.Context.PreviousPage.Value - 1),
		//			next: numbers.ElementAt(page.Context.NextPage.Value - 1),
		//			numbers: numbers),
		//		new PageModel(
		//			number: new NumberModel(page.Key.Number, _mesh.GetPageLink(page, page.Key.Number)),
		//			size: page.Key.Size,
		//			firstItem: new NumberModel(page.FirstItem, _mesh.GetItemLink(page, page.FirstItem)),
		//			lastItem: new NumberModel(page.LastItem, _mesh.GetItemLink(page, page.LastItem)),
		//			items: new ListItems(page.Items.Schema, GetItems(page, itemIdSelector))));
		//}

		//private IEnumerable<ListItem> GetItems(ListPage page, Func<ListItem, object> itemIdSelector)
		//{
		//	foreach(var item in page.Items)
		//	{
		//		var linkedItem = new ListItem(item.Number, item.Values);

		//		Mesh.LinkField.Set(linkedItem, _mesh.GetItemLink(itemIdSelector(item)));

		//		yield return linkedItem;
		//	}
		//}
	}
}