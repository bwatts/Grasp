using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp
{
	/// <summary>
	/// A notion qualified by a unique hierarchical name
	/// </summary>
	public abstract class NamedNotion : QualifiedNotion
	{
		/// <summary>
		/// Initializes a notion with the specified name, or anonymous if null
		/// </summary>
		/// <param name="name">The unique hierarchical name of the notion (defaults to <see cref="FullName.Anonymous"/>)</param>
		protected NamedNotion(FullName name = null)
		{
			Name = name ?? FullName.Anonymous;
		}

		/// <summary>
		/// Gets the unique hierarchical name of this notion in its effective context
		/// </summary>
		public FullName Name
		{
			get { return GetValue(FullName.NameField); }
			private set { SetValue(FullName.NameField, value); }
		}

		/// <summary>
		/// Gets this notion's name
		/// </summary>
		/// <returns>This notion's name</returns>
		protected override object GetQualifier()
		{
			return Name;
		}
	}
}