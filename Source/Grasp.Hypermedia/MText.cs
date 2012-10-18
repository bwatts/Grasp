using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MText : MContent
	{
		public static readonly Field<string> ValueField = Field.On<MText>.Backing(x => x.Value);

		public MText(string value, MClass @class) : base(@class)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public MText(string value) : this(value, MClass.None)
		{}

		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		protected override object GetHtmlContentWithoutClass()
		{
			return Value;
		}

		protected override object GetHtmlContentWithClass(string classStack)
		{
			return new XElement("span", new XAttribute("class", classStack), Value);
		}
	}
}