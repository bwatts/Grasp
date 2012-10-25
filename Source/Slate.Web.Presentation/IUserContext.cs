using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slate.Web.Presentation
{
	public interface IUserContext
	{
		UserIdentity GetIdentity();
	}
}