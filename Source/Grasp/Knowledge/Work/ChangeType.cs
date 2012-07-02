using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	public enum ChangeType
	{
		EntityCreated,
		EntityModified,
		EntityReconstituted,
		EntityDeleted,
		EntityAddedToSet,
		EntityRemovedFromSet,
		Field,
		Ownership
	}
}