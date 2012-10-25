using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// Describes a context in which fields are bound to values
	/// </summary>
	public interface IFieldContext
	{
		/// <summary>
		/// Gets all of the bindings in this context
		/// </summary>
		/// <returns>The bindings in this context</returns>
		IEnumerable<FieldBinding> GetBindings();

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
		/// Gets the value of the specified field, or its default value if not set
		/// </summary>
		/// <param name="field">The field to attempt to access</param>
		/// <param name="value">Set to the value of the specified field, or null if not set</param>
		/// <returns>Whether the field had a value</returns>
		bool TryGetValue(Field field, out object value);

		/// <summary>
		/// Gets the value of the specified field, or its default value if not set
		/// </summary>
		/// <typeparam name="T">The type to which to cast the value of the specified field</typeparam>
		/// <param name="field">The field to attempt to access</param>
		/// <param name="value">Set to the value of the specified field, or null if not set</param>
		/// <returns>Whether the field had a value</returns>
		bool TryGetValue<T>(Field<T> field, out T value);

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