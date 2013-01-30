using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Persistence
{
	/// <summary>
	/// Attaches persistent identifiers to <see cref="Notion"/> instances
	/// </summary>
	public static class PersistentId
	{
		public static readonly Trait ValueTrait = Trait.DeclaredBy(typeof(PersistentId));

		public static readonly Field<object> ValueField = ValueTrait.Field(() => ValueField);

		public static readonly object Local = new object();
	}
}