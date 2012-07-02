using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Infrastructure.Hosting
{
	public interface IDashProcess
	{
		Task StartAsync();

		Task StartAsync(CancellationToken cancellationToken);

		Task StartAsync(CancellationToken cancellationToken, TaskCreationOptions options);
	}
}