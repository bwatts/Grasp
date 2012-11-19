using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MValue : MContent
	{
		public static readonly Field<object> ObjectField = Field.On<MValue>.For(x => x.Object);
		public static readonly Field<bool> EscapedField = Field.On<MValue>.For(x => x.Escaped);

		public MValue(MClass @class, object @object, bool escaped = false) : base(@class)
		{
			Contract.Requires(@object != null);

			Object = @object;
			Escaped = escaped;
		}

		public MValue(object @object, bool escaped = false) : this(MClass.Empty, @object, escaped)
		{}

		public object Object { get { return GetValue(ObjectField); } private set { SetValue(ObjectField, value); } }
		public bool Escaped { get { return GetValue(EscapedField); } private set { SetValue(EscapedField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return !Escaped ? Object : new XCData(Object == null ? "" : Object.ToString());
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("span", new XAttribute("class", classStack), GetHtmlWithoutClass());
		}
	}
}