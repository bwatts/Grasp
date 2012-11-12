using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Grasp.Raven
{
	public class Revisions_ByAggregateId : AbstractIndexCreationTask<Revision>
	{
		public Revisions_ByAggregateId()
		{
			Map = revisions => from revision in revisions select new { AggregateId = revision.AggregateId };

			Sort(revision => revision.Number, SortOptions.Int);
		}
	}
}