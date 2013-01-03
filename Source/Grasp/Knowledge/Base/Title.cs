using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public class Title : ArticlePart
	{
		public static readonly Field<string> ContentField = Field.On<Title>.For(x => x.Content);

		public static readonly FullName PartName = new FullName("Title");

		public Title(string content) : base(PartName)
		{
			Content = content ?? "";
		}

		public string Content { get { return GetValue(ContentField); } private set { SetValue(ContentField, value); } }

		protected override string ToUnqualifiedString()
		{
			return Content;
		}
	}
}