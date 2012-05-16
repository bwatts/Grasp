using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	internal sealed class IsolatedNotionContext : INotionContext
	{
		private readonly IDictionary<Field, object> _values = new Dictionary<Field, object>();

		public object GetValue(Notion notion, Field field)
		{
			object value;

			_values.TryGetValue(field, out value);

			return value;
		}

		public void SetValue(Notion notion, Field field, object value)
		{
			_values[field] = value;
		}
	}
}