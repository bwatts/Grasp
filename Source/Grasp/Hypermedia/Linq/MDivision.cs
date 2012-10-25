using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MDivision : MContent
	{
		public static readonly Field<Many<MContent>> ChildrenField = Field.On<MDivision>.For(x => x.Children);

		public MDivision(MClass @class, IEnumerable<MContent> children) : base(@class)
		{
			Contract.Requires(children != null);

			Children = new Many<MContent>(children);
		}

		public MDivision(MClass @class, params MContent[] children) : this(@class, children as IEnumerable<MContent>)
		{}

		public Many<MContent> Children { get { return GetValue(ChildrenField); } private set { SetValue(ChildrenField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return new XElement("div", GetContent());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("div", new XAttribute("class", classStack), GetContent());
		}

		private IEnumerable<object> GetContent()
		{
			return Children.Select(child => child.GetHtml());
		}
	}
}