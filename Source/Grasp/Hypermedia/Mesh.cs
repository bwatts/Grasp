using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia
{
	/// <summary>
	/// Attaches an HTML link to <see cref="Notion"/> objects, allowing it to participate in the mesh of hypermedia
	/// </summary>
	public static class Mesh
	{
		public static readonly Field<HtmlLink> LinkField = Field.AttachedTo<Notion>.By.Static(typeof(Mesh)).For(() => LinkField);
	}
}