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
		public static readonly Field<Link> SelfLinkField = Field.AttachedTo<Notion>.By<Mesh>.For(x => GetSelfLink(x));
		public static readonly Field<Many<Link>> LinksField = Field.AttachedTo<Notion>.By<Mesh>.For(x => GetLinks(x));

		public static Link GetSelfLink(Notion notion)
		{
			return notion.GetValue(SelfLinkField);
		}

		public static void SetSelfLink(Notion notion, Link value)
		{
			notion.SetValue(SelfLinkField, value);
		}

		public static Many<Link> GetLinks(Notion notion)
		{
			return notion.GetValue(LinksField);
		}

		public static void SetLinks(Notion notion, Many<Link> value)
		{
			notion.SetValue(LinksField, value);
		}
	}
}