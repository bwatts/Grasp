using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class StartFormCommand : Command
	{
		public readonly Guid Id;
		public readonly string Name;

		public StartFormCommand(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}