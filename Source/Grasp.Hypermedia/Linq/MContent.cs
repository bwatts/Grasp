using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
{
	public abstract class MContent : Notion
	{
		public static readonly Field<MClass> ClassField = Field.On<MContent>.Backing(x => x.Class);

		protected MContent(MClass @class)
		{
			Contract.Requires(@class != null);

			Class = @class;
		}

		public MClass Class { get { return GetValue(ClassField); } private set { SetValue(ClassField, value); } }

		internal abstract object GetHtmlContent();
	}
}