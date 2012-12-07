using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Forms
{
	[ContractClass(typeof(IParseAlgorithmContract))]
	public interface IParseAlgorithm
	{
		ParseResult<T> ParseValue<T>(string value);
	}

	[ContractClassFor(typeof(IParseAlgorithm))]
	internal abstract class IParseAlgorithmContract : IParseAlgorithm
	{
		public ParseResult<T> ParseValue<T>(string value)
		{
			Contract.Requires(value != null);
			Contract.Ensures(Contract.Result<ParseResult<T>>() != null);

			return null;
		}
	}
}