using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public class MediaType : Notion, IEquatable<MediaType>
	{
		public static readonly Relationship Relationship = Resources.MediaTypeRelationship;
		public static readonly Relationship PluralRelationship = Resources.MediaTypePluralRelationship;

		public static readonly Field<string> NameField = Field.On<MediaType>.Backing(x => x.Name);

		public static bool operator ==(MediaType left, MediaType right)
		{
			return Object.ReferenceEquals(left, right) || (!Object.ReferenceEquals(left, null) && left.Equals(right));
		}

		public static bool operator !=(MediaType left, MediaType right)
		{
			return !(left == right);
		}

		public MediaType(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public bool Equals(MediaType other)
		{
			return other != null && Name.Equals(other.Name);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MediaType);
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