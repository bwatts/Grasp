using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work.Persistence;

namespace Grasp
{
	/// <summary>
	/// A persistent notion with an identifier of <see cref="EntityId"/> (equivalent to a <see cref="Guid"/>)
	/// </summary>
	public abstract class Entity : PersistentNotion<EntityId>
	{
		/// <summary>
		/// Initializes an entity with the specified identifier
		/// </summary>
		/// <param name="id">The identifier of the new entity</param>
		protected Entity(EntityId id = default(EntityId)) : base(id)
		{}
	}
}