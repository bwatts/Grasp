using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public class Section : ArticlePart
	{
		public static readonly Field<ManyInOrder<ArticlePart>> PartsField = Field.On<Section>.For(x => x.Parts);

		public Section(FullName name, IEnumerable<ArticlePart> parts = null) : base(name)
		{
			Parts = (parts ?? Enumerable.Empty<ArticlePart>()).ToManyInOrder();
		}

		public ManyInOrder<ArticlePart> Parts { get { return GetValue(PartsField); } private set { SetValue(PartsField, value); } }
	}
}