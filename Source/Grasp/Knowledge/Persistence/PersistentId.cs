using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Persistence
{
	/// <summary>
	/// Associates <see cref="Notion"/> instances with identifiers for a persistent context
	/// </summary>
	public sealed class PersistentId : Notion
	{
		/// <summary>
		/// Attaches persistent identifiers to <see cref="Notion"/> instances
		/// </summary>
		public static readonly Field<object> ValueField = Field.AttachedTo<Notion>.By<PersistentId>.For(x => GetValue(x));

		/// <summary>
		/// Gets the value of <see cref="ValueField"/>
		/// </summary>
		/// <param name="notion">The notion for which to get the value of <see cref="ValueField"/></param>
		/// <returns>The value of <see cref="ValueField"/></returns>
		public static object GetValue(Notion notion)
		{
			return notion.GetValue(ValueField);
		}

		/// <summary>
		/// Sets the value of <see cref="ValueField"/>
		/// </summary>
		/// <param name="notion">The notion on which to set the value of <see cref="ValueField"/></param>
		/// <param name="value">The new value of <see cref="ValueField"/></param>
		public static void SetValue(Notion notion, object value)
		{
			notion.SetValue(ValueField, value);
		}
	}
}