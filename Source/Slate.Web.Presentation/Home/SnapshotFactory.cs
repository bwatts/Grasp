using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia;

namespace Slate.Web.Presentation.Home
{
	public sealed class SnapshotFactory : ISnapshotFactory
	{
		public Task<SnapshotModel> CreateSnapshotAsync()
		{
			return Task.Run(() => new SnapshotModel(new Hyperlink("dashboard", "Dashboard", "View up-to-date information about your system")));
		}
	}
}