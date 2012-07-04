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
		public static readonly Field<object> SourceField = Field.On<Link>.Backing(x => x.Source);
		public static readonly Field<object> TargetField = Field.On<Link>.Backing(x => x.Target);
		public static readonly Field<Relationship> RelationshipField = Field.On<Link>.Backing(x => x.Relationship);

		public static Link Self(object source)
		{
			return new Link(source, source, Relationship.Self);
		}

		public Link(object source, object target, Relationship relationship)
		{
			Contract.Requires(source != null);
			Contract.Requires(target != null);
			Contract.Requires(relationship != null);

			Source = source;
			Target = target;
			Relationship = relationship;
		}

		public object Source { get { return GetValue(SourceField); } private set { SetValue(SourceField, value); } }
		public object Target { get { return GetValue(TargetField); } private set { SetValue(TargetField, value); } }
		public Relationship Relationship { get { return GetValue(RelationshipField); } private set { SetValue(RelationshipField, value); } }
	}
}