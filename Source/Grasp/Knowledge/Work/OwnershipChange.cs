using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	public sealed class OwnershipChange : Change
	{
		public static readonly Field<string> PriorOwnerField = Field.On<OwnershipChange>.Backing(x => x.PriorOwner);
		public static readonly Field<string> NewOwnerField = Field.On<OwnershipChange>.Backing(x => x.NewOwner);

		internal OwnershipChange() : base(ChangeType.Ownership)
		{}

		public string PriorOwner { get { return GetValue(PriorOwnerField); } }
		public string NewOwner { get { return GetValue(NewOwnerField); } }
	}
}