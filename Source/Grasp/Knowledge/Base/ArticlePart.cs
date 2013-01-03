using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public abstract class ArticlePart : NamedNotion
	{
		public ArticlePart(FullName name) : base(name)
		{}
	}
}