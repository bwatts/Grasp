using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Raven
{
	/// <summary>
	/// A Raven document representing a single revision in the history of an aggregate
	/// </summary>
	public class AggregateVersionDocument
	{
		public string Id { get; set; }

		public string AggregateDocumentId { get; set; }

		public int Number { get; set; }

		public List<Event> Events { get; set; }
	}
}