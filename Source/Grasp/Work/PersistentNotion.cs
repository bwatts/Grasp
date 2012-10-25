using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Work
{
	/// <summary>
	/// A notion with a persistent identifier
	/// </summary>
	/// <typeparam name="TId">The type of persistent identifier</typeparam>
	public abstract class PersistentNotion<TId> : Notion
	{
		protected PersistentNotion(TId id = default(TId))
		{
			SetValue(PersistentId.ValueField, id);
		}

		public TId Id
		{
			get { return (TId) GetValue(PersistentId.ValueField); }
		}

		public override string ToString()
		{
			return "{0} (ID: {1})".FormatCurrent(GetType().Name, Id);
		}
	}
}