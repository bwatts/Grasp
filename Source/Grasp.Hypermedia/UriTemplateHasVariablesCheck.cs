using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks;

namespace Grasp.Hypermedia
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class UriTemplateHasVariablesCheck
	{
		public static bool HasVariables(this ICheckable<UriTemplate> check)
		{
			return check.IsTrue(uri => uri.PathSegmentVariableNames.Any() || uri.QueryValueVariableNames.Any());
		}
	}
}