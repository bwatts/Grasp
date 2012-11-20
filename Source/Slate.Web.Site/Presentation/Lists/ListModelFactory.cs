using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
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

		public async Task<ListModel> CreateListModelAsync(Uri uri, ListViewKey pageKey)
		{
			var list = await _listClient.GetListAsync(uri, pageKey);

			var numbers = Enumerable
				.Range(1, list.Pages.Count.Value)
				.Select(number => new Count(number))
				.Select(number => new CountModel(number, _mesh.GetPageLink(list, number)))
				.ToList();

			return numbers.Count == 0 ? CreateEmptyListModel() : CreateListModel(list, numbers);
		}

		private ListModel CreateEmptyListModel()
		{
			return new ListModel(_emptyMessage);
		}

		private ListModel CreateListModel(Hyperlist list, List<CountModel> numbers)
		{
			return new ListModel(CreatePagesModel(list, numbers), CreateItemsModel(list));
		}

		private PagesModel CreatePagesModel(Hyperlist list, List<CountModel> numbers)
		{
			var currentPage = numbers.FirstOrDefault(number => number.Value == list.Pages.Current);

			var count = new CountModel(list.Pages.Count, _mesh.GetPageLink(list, list.Pages.Count));

			return currentPage == null
				? new PagesModel(count)
				: new PagesModel(
						count,
						currentPage,
						numbers.First(),
						numbers.Last(),
						currentPage.Value == 1 ? null : numbers[currentPage.Value.Value - 2],
						currentPage.Value == list.Pages.Count ? null : numbers[currentPage.Value.Value + 2],
						numbers);
		}

		private ItemsModel CreateItemsModel(Hyperlist list)
		{
			var firstItem = list.Items.First();
			var lastItem = list.Items.Last();

			return new ItemsModel(
				new CountModel(list.Items.Total, _mesh.GetItemCountLink(list)),
				new CountModel(firstItem.ListItem.Number, firstItem == null ? null : _mesh.GetItemNumberLink(list, firstItem)),
				new CountModel(lastItem.ListItem.Number, lastItem == null ? null : _mesh.GetItemNumberLink(list, lastItem)),
				list.Items.Schema,
				CreateItemModels(list));
		}

		private IEnumerable<ItemModel> CreateItemModels(Hyperlist list)
		{
			return list.Items.Select(item => new ItemModel(
				new CountModel(item.ListItem.Number, _mesh.GetItemLink(list, item)),
				item.ListItem.Bindings));
		}
	}
}