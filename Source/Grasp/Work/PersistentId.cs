using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Work
{
	/// <summary>
	/// Attaches persistent identifiers to <see cref="Notion"/> instances
	/// </summary>
	public static class PersistentId
	{
		/// <summary>
		/// The persistent identifier associated with <see cref="Notion"/> instances
		/// </summary>
		public static readonly Field<object> ValueField = Field.AttachedTo<Notion>.By.Static(typeof(PersistentId)).For(() => ValueField);
	}
}