using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge.Persistence;

namespace Grasp.Knowledge.Work
{
	public class ChangeSet : Notion
	{
		public static readonly Field<Many<Change>> ChangesField = Field.On<ChangeSet>.Backing(x => x.Changes);

		public Many<Change> Changes { get { return GetValue(ChangesField); } }
	}
}