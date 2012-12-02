using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	public class MultipleChoiceQuestion : ChoiceQuestion
	{
		public MultipleChoiceQuestion(IEnumerable<Choice> choices = null, FullName name = null) : base(choices, name)
		{}

		protected override Variable GetSelectionVariable(FullName name)
		{
			return new Variable<Many<string>>(name);
		}
	}
}