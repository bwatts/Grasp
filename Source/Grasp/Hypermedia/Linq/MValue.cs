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

		public MValue(MClass @class, object @object) : base(@class)
		{
			Contract.Requires(@object != null);

			Object = @object;
		}

		public MValue(object @object) : this(MClass.Empty, @object)
		{}

		public object Object { get { return GetValue(ObjectField); } private set { SetValue(ObjectField, value); } }

		protected override object GetHtmlWithoutClass()
		{
			return Object;
		}

		protected override object GetHtmlWithClass(string classStack)
		{
			return new XElement("span", new XAttribute("class", classStack), Object);
		}
	}
}