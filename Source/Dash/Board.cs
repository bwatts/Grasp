using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash
{
	public class Board : Notion
	{
		public static readonly Field<Many<Topic>> TopicsField = Field.On<Board>.Backing(x => x.Topics);

		public Many<Topic> Topics { get { return GetValue(TopicsField); } }
	}
}