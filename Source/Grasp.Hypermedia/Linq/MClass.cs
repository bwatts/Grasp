using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MClass : Notion
	{
		public static readonly Field<MClass> ParentField = Field.On<MClass>.Backing(x => x.Parent);
		public static readonly Field<string> NameField = Field.On<MClass>.Backing(x => x.Name);

		public MClass(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		private MClass(MClass parent, string name) : this(name)
		{
			Parent = parent;
		}

		public MClass Parent { get { return GetValue(ParentField); } private set { SetValue(ParentField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public bool IsEmpty
		{
			get { return Parent == null && Name == ""; }
		}

		public MClass Append(string name)
		{
			return new MClass(this, name);
		}

		internal XAttribute GetHtmlAttribute()
		{
			return IsEmpty ? null : new XAttribute("class", ToString());
		}

		public override string ToString()
		{
			return String.Join(" ", GetNamesToHierarchyRoot().Reverse());
		}

		private IEnumerable<string> GetNamesToHierarchyRoot()
		{
			yield return Name;

			var parent = Parent;

			while(parent != null)
			{
				yield return parent.Name;

				parent = parent.Parent;
			}
		}
	}
}