using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Work;

namespace Dash.Applications
{
	/// <summary>
	/// A participant in a Dash environment. Applications are residents of a persistent ecosystem.
	/// </summary>
	public class Application : NamedNotion
	{
		public static readonly Field<Many<FullName>> PackageNamesField = Field.On<Application>.For(x => x.PackageNames);

		public Application(FullName name) : base(name)
		{}

		public Many<FullName> PackageNames { get { return GetValue(PackageNamesField); } private set { SetValue(PackageNamesField, value); } }
	}
}