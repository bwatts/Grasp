using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dash.Context
{
	public interface ICompositionRoot<out T> : IDisposable
	{
		bool ValueResolved { get; }

		T Value { get; }
	}
}