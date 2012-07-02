using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash.Infrastructure.Hosting
{
	public class ErrorResult : RuntimeResult
	{
		public static readonly Field<Exception> ExceptionField = Field.On<ErrorResult>.Backing(x => x.Exception);

		public Exception Exception { get { return GetValue(ExceptionField); } }
	}
}