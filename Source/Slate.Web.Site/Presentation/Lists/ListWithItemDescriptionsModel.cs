using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Grasp;

namespace Slate.Web.Site.Presentation.Lists
{
	public class ListWithItemDescriptionsModel : ViewModel
	{
		public static readonly Field<ListModel> ListField = Field.On<ListWithItemDescriptionsModel>.For(x => x.List);
		public static readonly Field<Func<ItemModel, string>> _descriptionSelectorField = Field.On<ListWithItemDescriptionsModel>.For(x => x._descriptionSelector);

		private Func<ItemModel, string> _descriptionSelector { get { return GetValue(_descriptionSelectorField); } set { SetValue(_descriptionSelectorField, value); } }

		public ListWithItemDescriptionsModel(ListModel list, Func<ItemModel, string> descriptionSelector)
		{
			Contract.Requires(list != null);
			Contract.Requires(descriptionSelector != null);

			List = list;
			_descriptionSelector = descriptionSelector;
		}

		public ListModel List { get { return GetValue(ListField); } private set { SetValue(ListField, value); } }

		public string GetDescription(ItemModel item)
		{
			Contract.Requires(item != null);

			return _descriptionSelector(item);
		}
	}
}