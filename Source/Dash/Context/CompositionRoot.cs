using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dash.Context
{
	public abstract class CompositionRoot<T> : ICompositionRoot<T>
	{
		private readonly Lazy<T> _value;
		private bool _disposed;

		protected CompositionRoot()
		{
			_value = new Lazy<T>(ResolveValue);
		}

		public void Dispose()
		{
			if(!_disposed)
			{
				_disposed = true;

				Dispose(true);
			}
		}

		protected abstract void Dispose(bool disposing);

		public T Value
		{
			get { return _value.Value; }
		}

		public bool ValueResolved
		{
			get { return _value.IsValueCreated; }
		}

		protected abstract T ResolveValue();
	}
}