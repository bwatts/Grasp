using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
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

		internal override object GetHtmlContent()
		{
			return new XElement("div", GetDivisionContent());
		}

		private IEnumerable<object> GetDivisionContent()
		{
			var classAttribute = Class.GetHtmlAttribute();

			if(classAttribute != null)
			{
				yield return classAttribute;
			}

			foreach(var child in Children)
			{
				yield return child.GetHtmlContent();
			}
		}
	}
}