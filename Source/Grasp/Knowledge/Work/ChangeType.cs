using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	public enum ChangeType
	{
		EntityCreated,
		EntityReconstituted,
		EntityUpdated,
		EntityDeleted,
		EntityAddedToSet,
		EntityRemovedFromSet,
		Field
	}
}