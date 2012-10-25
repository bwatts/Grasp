using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation
{
	[ContractClass(typeof(ILayoutFactoryContract))]
	public interface ILayoutFactory
	{
		Task<ILayoutModel<TContent>> CreateLayoutAsync<TContent>(string title, TContent content, string currentAreaId = null, string currentSubAreaId = null);
	}

	[ContractClassFor(typeof(ILayoutFactory))]
	internal abstract class ILayoutFactoryContract : ILayoutFactory
	{
		Task<ILayoutModel<TContent>> ILayoutFactory.CreateLayoutAsync<TContent>(string title, TContent content, string currentAreaId, string currentSubAreaId)
		{
			Contract.Requires(title != null);
			Contract.Ensures(Contract.Result<Task<ILayoutModel<TContent>>>() != null);

			return null;
		}
	}
}