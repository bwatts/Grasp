using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;

namespace Slate.Http.Api
{
	public class FormResource : HttpResource
	{
		public static readonly Field<Guid> IdField = Field.On<FormResource>.For(x => x.Id);
		public static readonly Field<string> NameField = Field.On<FormResource>.For(x => x.Name);

		public FormResource(HttpResourceHeader header, Guid id, string name) : base(header)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Requires(name != null);

			Id = id;
			Name = name;
		}

		public Guid Id { get { return GetValue(IdField); } private set { SetValue(IdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}