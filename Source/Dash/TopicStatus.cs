using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash
{
	public class TopicStatus : ComparableNotion<TopicStatus>
	{
		public static readonly Field<string> NameField = Field.On<TopicStatus>.Backing(x => x.Name);

		public static readonly TopicStatus Default = new TopicStatus("");

		public TopicStatus(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public override int CompareTo(TopicStatus other)
		{
			return other == null ? 1 : Name.CompareTo(other.Name);
		}

		public override bool Equals(TopicStatus other)
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