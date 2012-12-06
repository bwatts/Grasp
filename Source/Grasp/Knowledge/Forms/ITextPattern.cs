using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Forms
{
	[ContractClass(typeof(ITextPatternContract))]
	public interface ITextPattern
	{
		Rule GetRule();
	}

	[ContractClassFor(typeof(ITextPattern))]
	internal abstract class ITextPatternContract : ITextPattern
	{
		Rule ITextPattern.GetRule()
		{
			Contract.Ensures(Contract.Result<Rule>() != null);

			return null;
		}
	}
}