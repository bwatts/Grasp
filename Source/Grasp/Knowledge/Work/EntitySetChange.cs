using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge.Persistence;

namespace Grasp.Knowledge.Work
{
	public sealed class EntitySetChange : Change
	{
		public static readonly Field<IEntitySet> EntitySetField = Field.On<EntitySetChange>.For(x => x.EntitySet);

		public new IEntitySet EntitySet { get { return GetValue(EntitySetField); } }

		internal EntitySetChange(ChangeType type) : base(type)
		{}
	}
}