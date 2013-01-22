using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Grasp.Json
{
	public interface IJsonStateFactory
	{
		JsonState CreateJsonState(JToken json);
	}
}