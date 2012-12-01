using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Definition
{
	public class SingleChoiceQuestion : ChoiceQuestion
	{
		public SingleChoiceQuestion(IEnumerable<Choice> choices = null, FullName name = null) : base(choices, name)
		{}

		protected override Variable GetSelectionVariable(FullName name)
		{
			return new Variable<string>(name);
		}
	}
}