﻿using System;
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
	public sealed class ListFactory : Notion, IListFactory
	{
		public static readonly Field<IListClient> _clientField = Field.On<ListFactory>.For(x => x._client);
		public static readonly Field<IListMesh> _meshField = Field.On<ListFactory>.For(x => x._mesh);
		public static readonly Field<string> _emptyMessageField = Field.On<ListFactory>.For(x => x._emptyMessage);

		private IListClient _client { get { return GetValue(_clientField); } set { SetValue(_clientField, value); } }
		private IListMesh _mesh { get { return GetValue(_meshField); } set { SetValue(_meshField, value); } }
		private string _emptyMessage { get { return GetValue(_emptyMessageField); } set { SetValue(_emptyMessageField, value); } }

		public ListFactory(IListClient client, IListMesh mesh, string emptyMessage)
		{
			Contract.Requires(client != null);
			Contract.Requires(mesh != null);
			Contract.Requires(emptyMessage != null);

			_client = client;
			_mesh = mesh;
			_emptyMessage = emptyMessage;
		}

		public async Task<ListModel> CreateListAsync(Uri uri, ListPageKey pageKey, Func<HyperlistItem, object> itemIdSelector)
		{
			var list = await _client.GetListAsync(uri, pageKey);

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
					firstItem: new NumberModel(list.Page.FirstItem, _mesh.GetItemLink(list, list.Page.FirstItem)),
					lastItem: new NumberModel(list.Page.LastItem, _mesh.GetItemLink(list, list.Page.LastItem)),
					items: new HyperlistItems(list.Page.Items.Schema, GetItems(list, itemIdSelector))));
		}

		private IEnumerable<HyperlistItem> GetItems(Hyperlist list, Func<HyperlistItem, object> itemIdSelector)
		{
			return list.Page.Items.Select(item => new HyperlistItem(_mesh.GetItemLink(itemIdSelector(item)), item.ListItem));
		}
	}
}