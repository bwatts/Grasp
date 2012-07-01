using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class Mesh
	{
		public static readonly Field<Link> SelfLinkField = Field.AttachedTo<Notion>.By<Mesh>.Backing(() => SelfLinkField);
		public static readonly Field<Many<Link>> LinksField = Field.AttachedTo<Notion>.By<Mesh>.Backing(() => LinksField);
	}
}