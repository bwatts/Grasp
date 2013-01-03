using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;

namespace Dash.Applications.Definition
{
	public class PackageSource : Notion
	{
		public static readonly Field<PackageSourceLocation> LocationField = Field.On<PackageSource>.For(x => x.Location);

		public PackageSource(PackageSourceLocation location)
		{
			Contract.Requires(location != null);

			Location = location;
		}

		public PackageSourceLocation Location { get { return GetValue(LocationField); } private set { SetValue(LocationField, value); } }
	}
}