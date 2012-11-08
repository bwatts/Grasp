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

namespace Slate.Web.Presentation.Lists
{
	public sealed class ListModelFactory : Notion, IListModelFactory
	{
		public static readonly Field<IListClient> _listClientField = Field.On<ListModelFactory>.For(x => x._listClient);
		public static readonly Field<IListMesh> _meshField = Field.On<ListModelFactory>.For(x => x._mesh);
		public static readonly Field<string> _emptyMessageField = Field.On<ListModelFactory>.For(x => x._emptyMessage);

		private IListClient _listClient { get { return GetValue(_listClientField); } set { SetValue(_listClientField, value); } }
		private IListMesh _mesh { get { return GetValue(_meshField); } set { SetValue(_meshField, value); } }
		private string _emptyMessage { get { return GetValue(_emptyMessageField); } set { SetValue(_emptyMessageField, value); } }

		public ListModelFactory(IListClient listClient, IListMesh mesh, string emptyMessage)
		{
			Contract.Requires(listClient != null);
			Contract.Requires(mesh != null);
			Contract.Requires(emptyMessage != null);

			_listClient = listClient;
			_mesh = mesh;
			_emptyMessage = emptyMessage;
		}

		public async Task<ListModel> CreateListModelAsync(Uri uri, ListPageKey pageKey, Func<HyperlistItem, object> itemIdSelector)
		{
			var list = await _listClient.GetListAsync(uri, pageKey);

			var numbers = Enumerable
				.Range(1, list.Context.PageCount.Value)
				.Select(number => new Number(number))
				.Select(number => new NumberModel(number, _mesh.GetPageLink(list, number)))
				.ToList();

			return numbers.Count == 0 ? CreateEmptyListModel(list) : CreateListModel(list, numbers, itemIdSelector);
		}

		private ListModel CreateEmptyListModel(Hyperlist list)
		{
			return new ListModel(new ContextModel(_mesh.GetCountLink(list)), _emptyMessage);
		}

		private ListModel CreateListModel(Hyperlist list, List<NumberModel> numbers, Func<HyperlistItem, object> itemIdSelector)
		{
			return new ListModel(
				new ContextModel(
					itemCount: new NumberModel(list.Context.ItemCount, _mesh.GetCountLink(list)),
					first: numbers.First(),
					last: numbers.Last(),
					previous: numbers.ElementAt(list.Context.PreviousPage.Value - 1),
					next: numbers.ElementAt(list.Context.NextPage.Value - 1),
					numbers: numbers),
				new PageModel(
					number: new NumberModel(list.Page.Number, _mesh.GetPageLink(list, list.Page.Number)),
					size: list.Page.Size,
					firstItem: new NumberModel(list.Page.FirstItemNumber, _mesh.GetItemLink(list, list.Page.GetFirstItem())),
					lastItem: new NumberModel(list.Page.LastItemNumber, _mesh.GetItemLink(list, list.Page.GetLastItem())),
					items: new HyperlistItems(list.Page.Items.Schema, GetItems(list))));
		}

		private IEnumerable<HyperlistItem> GetItems(Hyperlist list)
		{
			return list.Page.Items.Select(item => new HyperlistItem(_mesh.GetItemLink(list, item), item.ListItem));
		}
	}
}