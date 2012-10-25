using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message with the intent to initate an action
	/// </summary>
	public abstract class Command : Message
	{
		protected Command(Guid? id = null) : base(id)
		{}
	}
}