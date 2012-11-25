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
		/// Initializes an entity with the specified identifier or a default identifier if not specified
		/// </summary>
		/// <param name="id">The identifier of the new entity, or null to generate a new identifier</param>
		protected Entity(EntityId? id = null) : base(id ?? EntityId.Generate())
		{}
	}
}