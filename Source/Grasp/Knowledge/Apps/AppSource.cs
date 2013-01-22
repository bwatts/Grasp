using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps
{
	public sealed class AppSource : ComparableValue<AppSource, string>
	{
		public AppSource(string value) : base(value)
		{}
	}
}