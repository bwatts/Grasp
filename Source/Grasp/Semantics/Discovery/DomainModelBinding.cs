using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Semantics.Discovery
{
	public abstract class DomainModelBinding : Notion
	{
		public abstract DomainModel GetDomainModel();
	}
}