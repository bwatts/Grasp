using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation
{
	public class LayoutModel<TContent> : ViewModel, ILayoutModel<TContent>
	{
		public static readonly Field<UserIdentity> UserField = Field.On<LayoutModel<TContent>>.For(x => x.User);
		public static readonly Field<string> TitleField = Field.On<LayoutModel<TContent>>.For(x => x.Title);
		public static readonly Field<Hyperlink> SystemLinkField = Field.On<LayoutModel<TContent>>.For(x => x.SystemLink);
		public static readonly Field<NavigationModel> NavigationField = Field.On<LayoutModel<TContent>>.For(x => x.Navigation);
		public static readonly Field<TContent> ContentField = Field.On<LayoutModel<TContent>>.For(x => x.Content);

		public LayoutModel(UserIdentity user, string title, Hyperlink systemLink, NavigationModel navigation, TContent content)
		{
			Contract.Requires(user != null);
			Contract.Requires(title != null);
			Contract.Requires(systemLink != null);
			Contract.Requires(navigation != null);

			User = user;
			Title = title;
			SystemLink = systemLink;
			Navigation = navigation;
			Content = content;
		}

		public UserIdentity User { get { return GetValue(UserField); } private set { SetValue(UserField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public Hyperlink SystemLink { get { return GetValue(SystemLinkField); } private set { SetValue(SystemLinkField, value); } }
		public NavigationModel Navigation { get { return GetValue(NavigationField); } private set { SetValue(NavigationField, value); } }
		public TContent Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }

		object ILayoutModel.Content
		{
			get { return Content; }
		}
	}
}