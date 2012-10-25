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
	public class AggregateVersionDocuments_ByAggregateDocumentId : AbstractIndexCreationTask<AggregateVersionDocument>
	{
		public AggregateVersionDocuments_ByAggregateDocumentId()
		{
			Map = versions => from version in versions select new { version.AggregateDocumentId };

			Sort(version => version.Number, SortOptions.Int);
		}
	}
}