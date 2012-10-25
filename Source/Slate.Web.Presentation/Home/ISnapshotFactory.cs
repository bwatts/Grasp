using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slate.Web.Presentation.Home
{
	public interface ISnapshotFactory
	{
		Task<SnapshotModel> CreateSnapshotAsync();
	}
}