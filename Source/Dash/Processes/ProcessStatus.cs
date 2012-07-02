using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dash.Processes
{
	public class ProcessStatus : TopicStatus
	{
		public static readonly ProcessStatus Pending = new ProcessStatus("Pending");
		public static readonly ProcessStatus Waiting = new ProcessStatus("Waiting");
		public static readonly ProcessStatus Reminding = new ProcessStatus("Reminding");
		public static readonly ProcessStatus Over = new ProcessStatus("Over");

		protected ProcessStatus(string name) : base(name)
		{}
	}
}