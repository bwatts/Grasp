using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Checks;
using Grasp.Hypermedia;

namespace Slate.Web.Presentation.Navigation
{
	public class NavigationModel : ViewModel
	{
		public static readonly Field<Many<NavigationArea>> AreasField = Field.On<NavigationModel>.For(x => x.Areas);
		public static readonly Field<NavigationArea> CurrentAreaField = Field.On<NavigationModel>.For(x => x.CurrentArea);
		public static readonly Field<NavigationArea> CurrentSubAreaField = Field.On<NavigationModel>.For(x => x.CurrentSubArea);

		public NavigationModel(IEnumerable<NavigationArea> areas)
		{
			Contract.Requires(areas != null);

			Areas = new Many<NavigationArea>(areas);

			CurrentArea = Areas.FirstOrDefault();
		}

		public Many<NavigationArea> Areas { get { return GetValue(AreasField); } private set { SetValue(AreasField, value); } }
		public NavigationArea CurrentArea { get { return GetValue(CurrentAreaField); } private set { SetValue(CurrentAreaField, value); } }
		public NavigationArea CurrentSubArea { get { return GetValue(CurrentSubAreaField); } private set { SetValue(CurrentSubAreaField, value); } }

		public bool HasCurrentArea
		{
			get { return CurrentArea != null; }
		}

		public bool HasSubNavigation
		{
			get { return HasCurrentArea && CurrentArea.SubAreas.Any(); }
		}

		public void SelectAreas(string currentAreaId, string currentSubAreaId)
		{
			if(currentAreaId == null)
			{
				CurrentArea = null;
			}
			else
			{
				CurrentArea = Areas.FirstOrDefault(area => area.Value == currentAreaId);

				if(CurrentArea == null)
				{
					CurrentSubArea = null;
				}
				else if(currentSubAreaId == null)
				{
					CurrentSubArea = CurrentArea.SubAreas.FirstOrDefault();
				}
				else
				{
					CurrentSubArea = CurrentArea.SubAreas.First(subArea => subArea.Value == currentSubAreaId);
				}
			}
		}
	}
}