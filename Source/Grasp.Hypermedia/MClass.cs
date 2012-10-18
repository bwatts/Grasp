using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MClass : ComparableNotion<MClass>
	{
		public static readonly Field<string> NameField = Field.On<MClass>.Backing(x => x.Name);

		public static readonly MClass None = new MClass("");

		public MClass(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public bool IsNone
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