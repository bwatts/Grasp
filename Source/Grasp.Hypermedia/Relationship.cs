using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia
{
	public sealed class Relationship : ComparableValue<Relationship, string>, IStackable<Relationship>
	{
		public static readonly Relationship Empty = "";
		public static readonly Relationship Self = Resources.SelfRelationship;

		public static implicit operator Relationship(string value)
		{
			return new Relationship(value);
		}

		public static implicit operator string(Relationship relationship)
		{
			return relationship.ToString();
		}

		public Relationship(string value) : base(value)
		{}
	}
}