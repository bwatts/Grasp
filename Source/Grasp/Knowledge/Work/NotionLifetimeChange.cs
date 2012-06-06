using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge.Persistence;

namespace Grasp.Knowledge.Work
{
	public sealed class NotionLifetimeChange : Change
	{
		internal NotionLifetimeChange(ChangeType type) : base(type)
		{}
	}
}