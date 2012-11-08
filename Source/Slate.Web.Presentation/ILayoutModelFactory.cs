using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slate.Web.Presentation.Navigation;

namespace Slate.Web.Presentation
{
	[ContractClass(typeof(ILayoutModelFactoryContract))]
	public interface ILayoutModelFactory
	{
		Task<ILayoutModel<TContent>> CreateLayoutModelAsync<TContent>(string title, TContent content, string currentAreaId = null, string currentSubAreaId = null);
	}

	[ContractClassFor(typeof(ILayoutModelFactory))]
	internal abstract class ILayoutModelFactoryContract : ILayoutModelFactory
	{
		Task<ILayoutModel<TContent>> ILayoutModelFactory.CreateLayoutModelAsync<TContent>(string title, TContent content, string currentAreaId, string currentSubAreaId)
		{
			Contract.Requires(title != null);
			Contract.Ensures(Contract.Result<Task<ILayoutModel<TContent>>>() != null);

			return null;
		}
	}
}