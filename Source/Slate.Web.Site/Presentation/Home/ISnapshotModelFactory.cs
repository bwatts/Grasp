using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slate.Web.Site.Presentation.Home
{
	public interface ISnapshotModelFactory
	{
		Task<SnapshotModel> CreateSnapshotModelAsync();
	}
}