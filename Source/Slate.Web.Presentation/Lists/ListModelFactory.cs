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

		public async Task<ListModel> CreateListModelAsync(Uri uri, ListPageKey pageKey)
		{
			var list = await _listClient.GetListAsync(uri, pageKey);

			var numbers = Enumerable
				.Range(1, list.Context.PageCount.Value)
				.Select(number => new Number(number))
				.Select(number => new NumberModel(number, _mesh.GetPageLink(list, number)))
				.ToList();

			return numbers.Count == 0 ? CreateEmptyListModel(list) : CreateListModel(list, numbers);
		}

		private ListModel CreateEmptyListModel(Hyperlist list)
		{
			return new ListModel(new PageContextModel(_mesh.GetCountLink(list)), _emptyMessage);
		}

		private ListModel CreateListModel(Hyperlist list, List<NumberModel> numbers)
		{
			return new ListModel(CreatePageContextModel(list, numbers), CreatePageModel(list));
		}

		private PageContextModel CreatePageContextModel(Hyperlist list, List<NumberModel> numbers)
		{
			return new PageContextModel(
				new NumberModel(list.Context.ItemCount, _mesh.GetCountLink(list)),
				numbers.First(),
				numbers.Last(),
				numbers[list.Context.PreviousPage.Value - 1],
				numbers[list.Context.NextPage.Value - 1],
				numbers);
		}

		private PageModel CreatePageModel(Hyperlist list)
		{
			var firstItem = list.Page.GetFirstItem();
			var lastItem = list.Page.GetLastItem();

			return new PageModel(
				new NumberModel(list.Page.Number, _mesh.GetPageLink(list, list.Page.Number)),
				list.Page.Size,
				new NumberModel(list.Page.FirstItemNumber, firstItem == null ? null : _mesh.GetItemLink(list, firstItem)),
				new NumberModel(list.Page.LastItemNumber, lastItem == null ? null : _mesh.GetItemLink(list, lastItem)),
				list.Page.Items.Schema,
				CreateItemModels(list));
		}

		private IEnumerable<ItemModel> CreateItemModels(Hyperlist list)
		{
			return list.Page.Items.Select(item => new ItemModel(
				new NumberModel(item.ListItem.Number, _mesh.GetItemLink(list, item)),
				item.ListItem.Bindings));
		}
	}
}