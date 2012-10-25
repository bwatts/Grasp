using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// Describes a context which maintains the state of a <see cref="Notion"/>
	/// </summary>
	[ContractClass(typeof(INotionContextContract))]
	public interface INotionContext
	{
		/// <summary>
		/// Gets all bindings for the specified notion
		/// </summary>
		/// <param name="notion">The notion containing bound values</param>
		/// <returns>The bindings in effect for the specified notion</returns>
		IEnumerable<FieldBinding> GetBindings(Notion notion);

		/// <summary>
		/// Gets the value of the specified field for the specified notion
		/// </summary>
		/// <param name="notion">The notion containing the value of the specified field</param>
		/// <param name="field">The field of the value to retrieve</param>
		/// <returns>The value of the specified field associated with the specified notion</returns>
		object GetValue(Notion notion, Field field);

		/// <summary>
		/// Gets the value of the specified field for the specified notion, or its default if not set
		/// </summary>
		/// <param name="notion">The notion to check for a value of the specified field</param>
		/// <param name="field">The field of the value to retrieve</param>
		/// <returns>The value of the specified field associated with the specified notion, or its default if not set</returns>
		bool TryGetValue(Notion notion, Field field, out object value);

		/// <summary>
		/// Associates the specified value with the specified notion and field
		/// </summary>
		/// <param name="notion">The notion containing the value of the specified field</param>
		/// <param name="field">The field to set to the specified value</param>
		/// <param name="value">The value of the specified field to set for the specified notion</param>
		void SetValue(Notion notion, Field field, object value);
	}

	[ContractClassFor(typeof(INotionContext))]
	internal abstract class INotionContextContract : INotionContext
	{
		IEnumerable<FieldBinding> INotionContext.GetBindings(Notion notion)
		{
			Contract.Requires(notion != null);
			Contract.Ensures(Contract.Result<IEnumerable<FieldBinding>>() != null);

			return null;
		}

		object INotionContext.GetValue(Notion notion, Field field)
		{
			Contract.Requires(notion != null);
			Contract.Requires(field != null);

			return null;
		}

		bool INotionContext.TryGetValue(Notion notion, Field field, out object value)
		{
			Contract.Requires(notion != null);
			Contract.Requires(field != null);

			value = null;

			return false;
		}

		void INotionContext.SetValue(Notion notion, Field field, object value)
		{
			Contract.Requires(notion != null);
			Contract.Requires(field != null);
		}
	}
}