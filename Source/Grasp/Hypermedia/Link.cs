using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class Link : Notion
	{
		public static readonly Field<Notion> SourceField = Field.On<Link>.Backing(x => x.Source);
		public static readonly Field<Notion> TargetField = Field.On<Link>.Backing(x => x.Target);
		public static readonly Field<Relationship> RelationshipField = Field.On<Link>.Backing(x => x.Relationship);

		public static Link Self(Notion source)
		{
			return new Link(source, source, Relationship.Self);
		}

		public Link(Notion source, Notion target, Relationship relationship)
		{
			Contract.Requires(source != null);
			Contract.Requires(target != null);
			Contract.Requires(relationship != null);

			Source = source;
			Target = target;
			Relationship = relationship;
		}

		public Notion Source { get { return GetValue(SourceField); } private set { SetValue(SourceField, value); } }
		public Notion Target { get { return GetValue(TargetField); } private set { SetValue(TargetField, value); } }
		public Relationship Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }
	}
}