using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp
{
	/// <summary>
	/// A notion with a unique hierarchical name in its effective context
	/// </summary>
	public abstract class QualifiedNotion : Notion
	{
		/// <summary>
		/// Gets a qualified text representation of this this notion
		/// </summary>
		/// <returns>A qualified text representation of this this notion</returns>
		public override string ToString()
		{
			return Resources.QualifiedNotion.FormatInvariant(ToUnqualifiedString(), GetQualifier());
		}

		/// <summary>
		/// Gets an unqualified text representation of this this notion
		/// </summary>
		/// <returns>An unqualified text representation of this this notion</returns>
		protected virtual string ToUnqualifiedString()
		{
			return GetType().Name;
		}

		/// <summary>
		/// When implemented by a derived class, gets the qualifier of the text representation of this notion
		/// </summary>
		/// <returns>The qualifier of the text representation of this notion</returns>
		protected abstract object GetQualifier();
	}
}