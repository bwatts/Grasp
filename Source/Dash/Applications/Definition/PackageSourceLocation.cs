using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;

namespace Dash.Applications.Definition
{
	public sealed class PackageSourceLocation : ComparableValue<PackageSourceLocation, string>
	{
		public PackageSourceLocation(string value) : base(value)
		{}
	}
}