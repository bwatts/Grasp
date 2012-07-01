using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	/// <summary>
	/// Base implementation of an object which externalizes its state to a context
	/// </summary>
	public abstract class Notion : IFieldContext
	{
		/// <summary>
		/// Initializes a notion with an isolated context
		/// </summary>
		protected Notion()
		{
			Context = new InitialContext();
		}

		internal INotionContext Context { get; set; }

		object IFieldContext.GetValue(Field field)
		{
			return Context.GetValue(this, field);
		}

		T IFieldContext.GetValue<T>(Field<T> field)
		{
			return (T) Context.GetValue(this, field);
		}

		void IFieldContext.SetValue(Field field, object value)
		{
			Context.SetValue(this, field, value);
		}

		void IFieldContext.SetValue<T>(Field<T> field, T value)
		{
			Context.SetValue(this, field, value);
		}

		protected object GetValue(Field field)
		{
			return Context.GetValue(this, field);
		}

		protected T GetValue<T>(Field<T> field)
		{
			return (T) Context.GetValue(this, field);
		}

		protected void SetValue(Field field, object value)
		{
			Context.SetValue(this, field, value);
		}

		protected void SetValue<T>(Field<T> field, T value)
		{
			Context.SetValue(this, field, value);
		}

		private sealed class InitialContext : INotionContext
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
}