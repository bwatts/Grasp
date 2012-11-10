using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Navigation
{
	public sealed class NavigationArea : ComparableValue<NavigationArea, string>
	{
		public static readonly Field<Hyperlink> LinkField = Field.On<NavigationArea>.For(x => x.Link);
		public static readonly Field<Many<NavigationArea>> SubAreasField = Field.On<NavigationArea>.For(x => x.SubAreas);

		public NavigationArea(string id, Hyperlink link, IEnumerable<NavigationArea> subAreas = null) : base(id)
		{
			Contract.Requires(link != null);

			Link = link;
			SubAreas = (subAreas ?? Enumerable.Empty<NavigationArea>()).ToMany();
		}

		public Hyperlink Link { get { return GetValue(LinkField); } private set { SetValue(LinkField, value); } }
		public Many<NavigationArea> SubAreas { get { return GetValue(SubAreasField); } private set { SetValue(SubAreasField, value); } }
	}
}