using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;
using Grasp.Work;

namespace Slate.Forms
{
	public sealed class StartFormHandler : Notion, IHandler<StartFormCommand>
	{
		public static readonly Field<IRepository<Form>> _formRepositoryField = Field.On<StartFormHandler>.For(x => x._formRepository);

		private IRepository<Form> _formRepository { get { return GetValue(_formRepositoryField); } set { SetValue(_formRepositoryField, value); } }

		public StartFormHandler(IRepository<Form> formRepository)
		{
			Contract.Requires(formRepository != null);

			_formRepository = formRepository;
		}

		public async Task HandleAsync(StartFormCommand command)
		{
			var form = new Form(command.FormId, command.Name);

			await _formRepository.SaveAsync(form);
		}
	}
}