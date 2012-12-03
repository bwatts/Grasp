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
		public static Schema Merge(this IEnumerable<Schema> schemas, SchemaMergeRule variableRule = SchemaMergeRule.ErrorOnConflict, SchemaMergeRule calculationRule = SchemaMergeRule.ErrorOnConflict)
		{
			Contract.Requires(schemas != null);

			return schemas.DefaultIfEmpty(Schema.Empty).Aggregate((left, right) => left.Merge(right, variableRule, calculationRule));
		}
	}
}