using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	/// <summary>
	/// Base implementation of an object which externalizes its state to a context
	/// </summary>
	public abstract class Notion
	{
		/// <summary>
		/// Initializes a notion with an isolated context
		/// </summary>
		protected Notion()
		{
			Context = new IsolatedNotionContext();
		}

		internal INotionContext Context { get; set; }

		/// <summary>
		/// Gets the value of the specified field
		/// </summary>
		/// <param name="field">The field to access</param>
		/// <returns>The value of the specified field</returns>
		public object GetValue(Field field)
		{
			return Context.GetValue(this, field);
		}

		/// <summary>
		/// Gets the value of the specified field
		/// </summary>
		/// <typeparam name="T">The type to which to cast the value of the specified field</typeparam>
		/// <param name="field">The field to access</param>
		/// <returns>The value of the specified field</returns>
		public T GetValue<T>(Field<T> field)
		{
			return (T) Context.GetValue(this, field);
		}

		/// <summary>
		/// Associates the specified value the specified field
		/// </summary>
		/// <param name="field">The field to set</param>
		/// <param name="value">The new value of the specified field</param>
		public void SetValue(Field field, object value)
		{
			Context.SetValue(this, field, value);
		}

		/// <summary>
		/// Associates the specified value the specified field
		/// </summary>
		/// <typeparam name="T">the type of value represented by the specified field</typeparam>
		/// <param name="field">The field to set</param>
		/// <param name="value">The new value of the specified field</param>
		public void SetValue<T>(Field<T> field, T value)
		{
			Context.SetValue(this, field, value);
		}
	}
}