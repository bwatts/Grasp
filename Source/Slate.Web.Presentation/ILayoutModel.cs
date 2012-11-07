using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation
{
	public interface ILayoutModel
	{
		UserIdentity User { get; }

		string Title { get; }

		Hyperlink SystemLink { get; }

		NavigationModel Navigation { get; }

		object Content { get; }
	}

	public interface ILayoutModel<TContent> : ILayoutModel
	{
		new TContent Content { get; }
	}
}