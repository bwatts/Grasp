using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class Relationship : Notion, IEquatable<Relationship>
	{
		public static readonly Relationship Default = Resources.DefaultRelationship;
		public static readonly Relationship Self = Resources.SelfRelationship;
		public static readonly Relationship Meta = Resources.MetaRelationship;

		public static readonly Relationship Singular = Resources.SingularRelationship;
		public static readonly Relationship Plural = Resources.PluralRelationship;

		public static readonly Field<string> NameField = Field.On<Relationship>.Backing(x => x.Name);

		public static implicit operator string(Relationship relationship)
		{
			return relationship.ToString();
		}

		public static implicit operator Relationship(string relationship)
		{
			return String.IsNullOrEmpty(relationship) ? Relationship.Default : new Relationship(relationship);
		}

		public static bool operator ==(Relationship left, Relationship right)
		{
			return Object.ReferenceEquals(left, right) || (!Object.ReferenceEquals(left, null) && left.Equals(right));
		}

		public static bool operator !=(Relationship left, Relationship right)
		{
			return !(left == right);
		}

		public static IEqualityComparer<string> NameComparer
		{
			get { return StringComparer.OrdinalIgnoreCase; }
		}

		public Relationship(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public bool Equals(Relationship other)
		{
			return other != null && NameComparer.Equals(Name, other.Name);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Relationship);
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