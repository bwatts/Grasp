using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks
{
	/// <summary>
	/// Determines how the minimum and maximum are handled during range comparisons
	/// </summary>
	public enum BoundaryType
	{
		/// <summary>
		/// The range includes both the minimum and maximum
		/// </summary>
		Inclusive,

		/// <summary>
		/// The range includes the maximum but not the minimum
		/// </summary>
		ExcludeMinimum,

		/// <summary>
		/// The range includes the minimum but not the maximum
		/// </summary>
		ExcludeMaximum,

		/// <summary>
		/// The range does not include either the minimum or the maximum
		/// </summary>
		Exclusive
	}
}