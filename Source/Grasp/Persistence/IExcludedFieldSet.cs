using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Persistence
{
	public interface IExcludedFieldSet
	{
		bool IsExcluded(Field field);
	}
}