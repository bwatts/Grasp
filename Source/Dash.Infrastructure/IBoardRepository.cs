using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dash.Infrastructure
{
	public interface IBoardRepository
	{
		Board GetBoard(string username);
	}
}