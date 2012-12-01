using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge
{
	public static class SchemaMerging
	{
		public static Schema Merge(this IEnumerable<Schema> schemas)
		{
			Contract.Requires(schemas != null);

			return schemas.Aggregate((left, right) => left.Merge(right));
		}
	}
}