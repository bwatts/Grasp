using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp;

namespace Slate.Web.Presentation.Lists
{
	public class ListModel : ViewModel
	{
		public static readonly Field<ContextModel> PagesField = Field.On<ListModel>.For(x => x.Context);
		public static readonly Field<PageModel> PageField = Field.On<ListModel>.For(x => x.Page);
		public static readonly Field<string> EmptyMessageField = Field.On<ListModel>.For(x => x.EmptyMessage);

		public ListModel(ContextModel context, PageModel page)
		{
			Contract.Requires(context != null);
			Contract.Requires(page != null);

			Context = context;
			Page = page;
		}

		public ListModel(ContextModel context, string emptyMessage)
		{
			Contract.Requires(context != null);
			Contract.Requires(emptyMessage != null);

			Context = context;
			EmptyMessage = emptyMessage;
		}

		public ContextModel Context { get { return GetValue(PagesField); } private set { SetValue(PagesField, value); } }
		public PageModel Page { get { return GetValue(PageField); } private set { SetValue(PageField, value); } }
		public string EmptyMessage { get { return GetValue(EmptyMessageField); } private set { SetValue(EmptyMessageField, value); } }

		public bool IsEmpty
		{
			get { return Page == null; }
		}
	}
}