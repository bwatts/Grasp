using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MText : MContent
	{
		public static readonly Field<string> ValueField = Field.On<MText>.Backing(x => x.Value);

		public MText(MClass @class, string value) : base(@class)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public string Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		internal override object GetHtmlContent()
		{
			var classAttribute = Class.GetHtmlAttribute();

			return classAttribute == null ? Value : new XElement("span", classAttribute, Value) as object;
		}
	}
}