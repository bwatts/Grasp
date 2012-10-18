using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MDivision : MContent
	{
		public static Field<Many<MContent>> ChildrenField = Field.On<MDivision>.Backing(x => x.Children);

		public MDivision(MClass @class, IEnumerable<MContent> children) : base(@class)
		{
			Contract.Requires(children != null);

			Children = new Many<MContent>(children);
		}

		public Many<MContent> Children { get { return GetValue(ChildrenField); } private set { SetValue(ChildrenField, value); } }

		protected override object GetHtmlContentWithoutClass()
		{
			return new XElement("div", GetContent());
		}

		protected override object GetHtmlContentWithClass(string classStack)
		{
			return new XElement("div", new XAttribute("class", classStack), GetContent());
		}

		private IEnumerable<object> GetContent()
		{
			return Children.Select(child => child.GetHtmlContent());
		}
	}
}