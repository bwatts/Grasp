using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;

namespace Slate.Web.Presentation
{
	public class UserIdentity : Notion
	{
		public static readonly Field<string> NameField = Field.On<UserIdentity>.For(x => x.Name);
		public static readonly Field<Hyperlink> LinkField = Field.On<UserIdentity>.For(x => x.Link);

		public UserIdentity(string name, Hyperlink link)
		{
			Contract.Requires(name != null);
			Contract.Requires(link != null);

			Name = name;
			Link = link;
		}

		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Hyperlink Link { get { return GetValue(LinkField); } private set { SetValue(LinkField, value); } }

		public bool IsAuthenticated
		{
			get { return Name != ""; }
		}
	}
}