using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class PollWorkCommand : Command
	{
		public PollWorkCommand(FullName workItemName) : base(workItemName)
		{
			
		}
	}
}