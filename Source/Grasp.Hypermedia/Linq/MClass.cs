using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cloak;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MClass : ComparableNotion<MClass>, IStackable<MClass>
	{
		public static readonly Field<string> NameField = Field.On<MClass>.For(x => x.Name);

		public static readonly MClass Empty = new MClass("");

		public static implicit operator string(MClass @class)
		{
			return @class.Name;
		}

		public static implicit operator MClass(string text)
		{
			return new MClass(text);
		}

		public MClass(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public bool IsEmpty
		{
			get { return Name == ""; }
		}

		public override string ToString()
		{
			return Name;
		}

		public override int CompareTo(MClass other)
		{
			return other == null ? 1 : Name.CompareTo(other.Name);
		}

		public override bool Equals(MClass other)
		{
			return other != null && Name.Equals(other.Name);
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
	}
}