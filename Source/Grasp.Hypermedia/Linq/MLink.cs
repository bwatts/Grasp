using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MLink : MContent
	{
		public static readonly Field<HttpLink> ValueField = Field.On<MLink>.Backing(x => x.Value);

		public MLink(MClass @class, HttpLink value) : base(@class)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		public HttpLink Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		internal override object GetHtmlContent()
		{
			var htmlLink = Value.ToHtmlLink("a");

			var classAttribute = Class.GetHtmlAttribute();

			if(classAttribute != null)
			{
				htmlLink.Add(classAttribute);
			}

			return htmlLink;
		}
	}
}