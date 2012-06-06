﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge
{
	/// <summary>
	/// Describes a context which maintains the state of a <see cref="Notion"/>
	/// </summary>
	[ContractClass(typeof(INotionContextContract))]
	public interface INotionContext
	{
		/// <summary>
		/// Gets the value of the specified field for the specified notion
		/// </summary>
		/// <param name="notion">The notion containing the value of the specified field</param>
		/// <param name="field">The field of the value to retrieve</param>
		/// <returns>The value of the specified field associated with the specified notion</returns>
		object GetValue(Notion notion, Field field);

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
		object INotionContext.GetValue(Notion notion, Field field)
		{
			Contract.Requires(notion != null);
			Contract.Requires(field != null);

			return null;
		}

		void INotionContext.SetValue(Notion notion, Field field, object value)
		{
			Contract.Requires(notion != null);
			Contract.Requires(field != null);
		}
	}
}