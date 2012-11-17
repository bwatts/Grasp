using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public class ApiErrorResource : HttpResource, IApiError
	{
		public static readonly Field<string> MessageField = Field.On<ApiErrorResource>.For(x => x.Message);
		public static readonly Field<long> CodeField = Field.On<ApiErrorResource>.For(x => x.Code);

		public ApiErrorResource(MHeader header, string message = "An error occurred", long code = 0) : base(header)
		{
			Contract.Requires(message != null);

			Message = message;
			Code = code;
		}

		public string Message { get { return GetValue(MessageField); } private set { SetValue(MessageField, value); } }
		public long Code { get { return GetValue(CodeField); } private set { SetValue(CodeField, value); } }
	}
}