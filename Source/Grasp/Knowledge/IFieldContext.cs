using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	/// <summary>
	/// Describes a context in which fields are bound to values
	/// </summary>
	public interface IFieldContext
	{
		/// <summary>
		/// Gets the value of the specified field
		/// </summary>
		/// <param name="field">The field to access</param>
		/// <returns>The value of the specified field</returns>
		object GetValue(Field field);

		/// <summary>
		/// Gets the value of the specified field
		/// </summary>
		/// <typeparam name="T">The type to which to cast the value of the specified field</typeparam>
		/// <param name="field">The field to access</param>
		/// <returns>The value of the specified field</returns>
		T GetValue<T>(Field<T> field);

		/// <summary>
		/// Associates the specified value the specified field
		/// </summary>
		/// <param name="field">The field to set</param>
		/// <param name="value">The new value of the specified field</param>
		void SetValue(Field field, object value);

		/// <summary>
		/// Associates the specified value the specified field
		/// </summary>
		/// <typeparam name="T">the type of value represented by the specified field</typeparam>
		/// <param name="field">The field to set</param>
		/// <param name="value">The new value of the specified field</param>
		void SetValue<T>(Field<T> field, T value);
	}
}