using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Work.Persistence
{
	/// <summary>
	/// A notion qualified by a unique persistent identifier
	/// </summary>
	/// <typeparam name="TId">The type of persistent identifier</typeparam>
	public abstract class PersistentNotion<TId> : QualifiedNotion
	{
		/// <summary>
		/// Initializes a notion with the specified persistent identifier
		/// </summary>
		/// <param name="id">The persistent identifier of the notion</param>
		protected PersistentNotion(TId id = default(TId))
		{
			Id = id;
		}

		/// <summary>
		/// Gets this notion's persistent identifier
		/// </summary>
		public TId Id
		{
			get { return (TId) GetValue(PersistentId.ValueField); }
			private set { SetValue(PersistentId.ValueField, value); }
		}

		/// <summary>
		/// Gets this notion's persistent identifier
		/// </summary>
		/// <returns>This notion's persistent identifier</returns>
		protected override object GetQualifier()
		{
			return Id;
		}
	}
}