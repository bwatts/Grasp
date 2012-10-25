using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Raven
{
	/// <summary>
	/// A Raven document representing the the most authoritative version of an aggregate in a particular context
	/// </summary>
	public class AggregateDocument
	{
		public string Id { get; set; }

		public int LatestVersion { get; set; }

		public List<string> VersionDocumentIds { get; set; }
	}
}