﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;
using Grasp.Work;
using Slate.Forms;

namespace Slate.Services
{
	public sealed class StartTestingHandler : Notion, IHandler<StartTestingCommand>
	{
		public static readonly Field<IRepository<Form>> _formRepositoryField = Field.On<StartTestingHandler>.For(x => x._formRepository);

		private IRepository<Form> _formRepository { get { return GetValue(_formRepositoryField); } set { SetValue(_formRepositoryField, value); } }

		public StartTestingHandler(IRepository<Form> formRepository)
		{
			Contract.Requires(formRepository != null);

			_formRepository = formRepository;
		}

		public async Task HandleAsync(StartTestingCommand c)
		{
			var form = await _formRepository.LoadAsync(c.FormId);

			form.Handle(c);

			await _formRepository.SaveAsync(form);
		}
	}
}