using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Dash
{
	public class UserDash : Notion
	{
		public static readonly Field<string> UsernameField = Field.On<UserDash>.Backing(x => x.Username);
		public static readonly Field<Many<Topic>> TopicsField = Field.On<UserDash>.Backing(x => x.Topics);

		public string Username { get { return GetValue(UsernameField); } }
		public Many<Topic> Topics { get { return GetValue(TopicsField); } }
	}
}