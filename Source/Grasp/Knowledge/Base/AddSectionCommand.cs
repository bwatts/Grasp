using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Base
{
	public class AddSectionCommand : ArticleCommand
	{
		public static readonly Field<FullName> SectionNameField = Field.On<AddSectionCommand>.For(x => x.SectionName);

		public AddSectionCommand(FullName workItemName, FullName articleName, FullName sectionName) : base(workItemName, articleName)
		{
			Contract.Requires(sectionName != null);

			SectionName = sectionName;
		}

		public FullName SectionName { get { return GetValue(SectionNameField); } private set { SetValue(SectionNameField, value); } }
	}
}