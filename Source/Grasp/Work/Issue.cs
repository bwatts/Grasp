using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Work
{
	/// <summary>
	/// An issue preventing work from continuing
	/// </summary>
	public class Issue : NamedNotion
	{
		public static readonly Field<string> DescriptionField = Field.On<Issue>.For(x => x.Description);

		/// <summary>
		/// Initializes an issue with the specified name and description
		/// </summary>
		/// <param name="name">The unique hierarchical name of the issue</param>
		/// <param name="description">A detailed description of the issue</param>
		public Issue(FullName name, string description) : base(name)
		{
			Contract.Requires(description != null);

			Description = description;
		}

		/// <summary>
		/// Gets a detailed description of the issue
		/// </summary>
		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
	}
}