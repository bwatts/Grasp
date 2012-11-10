using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Slate.Forms;

namespace Slate.Services
{
	public sealed class StartFormService : Publisher, IStartFormService
	{
		public async Task<Guid> StartFormAsync(string name)
		{
			var id = Guid.NewGuid();

			await IssueAsync(new StartFormCommand(id, name));

			return id;
		}
	}
}