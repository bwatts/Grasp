using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp;

namespace Slate.Web.Site.Presentation.Lists
{
	public class ListModel : ViewModel
	{
		public static readonly Field<PagesModel> PagesField = Field.On<ListModel>.For(x => x.Pages);
		public static readonly Field<ItemsModel> ItemsField = Field.On<ListModel>.For(x => x.Items);
		public static readonly Field<string> EmptyMessageField = Field.On<ListModel>.For(x => x.EmptyMessage);

		public ListModel(PagesModel pages, ItemsModel items)
		{
			Contract.Requires(pages != null);
			Contract.Requires(items != null);

			Pages = pages;
			Items = items;
		}

		public ListModel(string emptyMessage)
		{
			Contract.Requires(emptyMessage != null);

			EmptyMessage = emptyMessage;
		}

		public PagesModel Pages { get { return GetValue(PagesField); } private set { SetValue(PagesField, value); } }
		public ItemsModel Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
		public string EmptyMessage { get { return GetValue(EmptyMessageField); } private set { SetValue(EmptyMessageField, value); } }

		public bool IsEmpty
		{
			get { return Pages == null; }
		}
	}
}