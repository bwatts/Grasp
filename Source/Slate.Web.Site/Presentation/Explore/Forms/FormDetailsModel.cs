using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp;

namespace Slate.Web.Site.Presentation.Explore.Forms
{
	public class FormDetailsModel : ViewModel
	{
		public static readonly Field<string> NameField = Field.On<FormDetailsModel>.For(x => x.Name);

		public FormDetailsModel(string name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}