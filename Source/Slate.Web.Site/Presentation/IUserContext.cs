using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slate.Web.Site.Presentation
{
	public interface IUserContext
	{
		UserIdentity GetIdentity();
	}
}