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
		public static readonly Field<PageContextModel> PageContextField = Field.On<ListModel>.For(x => x.PageContext);
		public static readonly Field<PageModel> PageField = Field.On<ListModel>.For(x => x.Page);
		public static readonly Field<string> EmptyMessageField = Field.On<ListModel>.For(x => x.EmptyMessage);

		public ListModel(PageContextModel pageContext, PageModel page)
		{
			Contract.Requires(pageContext != null);
			Contract.Requires(page != null);

			PageContext = pageContext;
			Page = page;
		}

		public ListModel(PageContextModel pageContext, string emptyMessage)
		{
			Contract.Requires(pageContext != null);
			Contract.Requires(emptyMessage != null);

			PageContext = pageContext;
			EmptyMessage = emptyMessage;
		}

		public PageContextModel PageContext { get { return GetValue(PageContextField); } private set { SetValue(PageContextField, value); } }
		public PageModel Page { get { return GetValue(PageField); } private set { SetValue(PageField, value); } }
		public string EmptyMessage { get { return GetValue(EmptyMessageField); } private set { SetValue(EmptyMessageField, value); } }

		public bool IsEmpty
		{
			get { return Page == null; }
		}
	}
}