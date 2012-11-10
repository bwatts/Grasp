using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia;
using Slate.Web.Site.Presentation.Navigation;

namespace Slate.Web.Site.Presentation
{
	public sealed class LayoutModelFactory : Notion, ILayoutModelFactory
	{
		public static readonly Field<IUserContext> _userContextField = Field.On<LayoutModelFactory>.For(x => x._userContext);

		private IUserContext _userContext { get { return GetValue(_userContextField); } set { SetValue(_userContextField, value); } }

		public LayoutModelFactory(IUserContext userContext)
		{
			Contract.Requires(userContext != null);

			_userContext = userContext;
		}

		public Task<ILayoutModel<TContent>> CreateLayoutModelAsync<TContent>(string title, TContent content, string currentAreaId, string currentSubAreaId)
		{
			return Task.Run<ILayoutModel<TContent>>(() => new LayoutModel<TContent>(
				_userContext.GetIdentity(),
				title,
				new Hyperlink("", content: "Slate", title: "Your data has personality. Get to know it."),
				CreateNavigationModel(currentAreaId, currentSubAreaId),
				content));
		}

		private static NavigationModel CreateNavigationModel(string currentAreaId, string currentSubAreaId)
		{
			var navigation = new NavigationModel(CreateNavigationAreas().ToMany());

			navigation.SelectAreas(currentAreaId, currentSubAreaId);

			return navigation;
		}

		private static IEnumerable<NavigationArea> CreateNavigationAreas()
		{
			yield return new NavigationArea("home", new Hyperlink("", content: "home", title: "A high-level view of your system"));

			yield return new NavigationArea(
				"explore",
				new Hyperlink("explore", content: "explore", title: "Explore your system"),
				Params.Of(
					new NavigationArea("all", new Hyperlink("explore", "all", "Explore your forms and responses")),
					new NavigationArea("forms", new Hyperlink("explore/forms", "forms", "Explore your forms")),
					new NavigationArea("responses", new Hyperlink("explore/responses", "responses", "Explore your responses"))));

			yield return new NavigationArea(
				"issues",
				new Hyperlink("issues", content: "issues", title: "No one is perfect. Get closer than ever before with intrinsic issue tracking."),
				Params.Of(
				new NavigationArea("all", new Hyperlink("issues", "all", "Manage all issues")),
				new NavigationArea("open", new Hyperlink("issues/open", "open", "Manage open issues")),
				new NavigationArea("mine", new Hyperlink("issues/mine", "mine", "Manage your issues"))));

			yield return new NavigationArea("settings", new Hyperlink("settings", content: "settings", title: "Plenty of knobs and switches to get things just how you like"));

			yield return new NavigationArea("help", new Hyperlink("help", content: "help", title: "Everything you need to run Slate like a pro"));
		}
	}
}