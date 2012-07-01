using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Persistence
{
	/// <summary>
	/// Associates <see cref="Notion"/> instances with identifiers for a persistent context
	/// </summary>
	public sealed class PersistentId : Notion
	{
		/// <summary>
		/// Attaches persistent identifiers to <see cref="Notion"/> instances
		/// </summary>
		public static readonly Field<object> ValueField = Field.AttachedTo<Notion>.By<PersistentId>.Backing(() => ValueField);
	}
}