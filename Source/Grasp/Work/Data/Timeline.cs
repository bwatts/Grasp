using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work.Data
{
	public abstract class Timeline : NamedNotion
	{
		protected Timeline(FullName name) : base(name)
		{}

		public abstract History GetHistory();

		public abstract Task<Timeline> BranchAsync();

		public abstract Task MergeAsync(Timeline otherTimeline);
	}
}