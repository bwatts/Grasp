using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Lists;
using Raven.Client;
using Raven.Client.Linq;

namespace Grasp.Raven
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ListPages
	{
		public static IQueryable<T> TakePage<T>(this IRavenQueryable<T> ravenQuery, ListViewKey key, out Count totalItems)
		{
			Contract.Requires(ravenQuery != null);
			Contract.Requires(key != null);

			// TODO: Sorting

			RavenQueryStatistics statistics;

			var query = ravenQuery
				.Statistics(out statistics)
				.Skip((key.Start.Value - 1) * key.Size.Value)
				.Take(key.Size.Value)
				.ToList()
				.AsQueryable();

			totalItems = new Count(statistics.TotalResults);

			return query;
		}
	}
}