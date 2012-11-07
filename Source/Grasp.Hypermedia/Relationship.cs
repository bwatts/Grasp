﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Hypermedia
{
	public sealed class Relationship : ComparableNotion<Relationship>, IStackable<Relationship>
	{
		public static readonly Field<string> NameField = Field.On<Relationship>.For(x => x.Name);

		public static readonly Relationship Empty = new Relationship();
		public static readonly Relationship Self = new Relationship(Resources.SelfRelationship);

		public static readonly Relationship Singular = new Relationship(Resources.SingularRelationship);
		public static readonly Relationship Plural = new Relationship(Resources.PluralRelationship);

		public Relationship(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		private Relationship() : this("")
		{}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public override int CompareTo(Relationship other)
		{
			return other == null ? 1 : Name.CompareTo(other.Name);
		}

		public override bool Equals(Relationship other)
		{
			return other != null && Name.Equals(other.Name);
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}