using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Hypermedia.Linq
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ContentReading
	{
		public static T ReadContent<T>(this IEnumerable<MContent> contents, MClass @class = null) where T : MContent
		{
			Contract.Requires(contents != null);

			return @class == null
				? contents.OfType<T>().First()
				: contents.OfType<T>().First(content => content.Class.ContainsItem(@class));
		}

		public static T ReadValue<T>(this IEnumerable<MContent> contents, MClass @class = null)
		{
			Contract.Requires(contents != null);

			return contents.ReadContent<MValue>(@class).Read<T>();
		}

		public static T Read<T>(this MContent content)
		{
			Contract.Requires(content != null);

			return ((MValue) content).Read<T>();
		}

		public static T Read<T>(this MValue value)
		{
			Contract.Requires(value != null);

			return ChangeType.To<T>(value.Object);
		}

		public static MLink ReadLink(this IEnumerable<MContent> contents, Relationship relationship = null)
		{
			Contract.Requires(contents != null);

			return relationship == null
				? contents.OfType<MLink>().First()
				: contents.OfType<MLink>().First(content => content.Hyperlink.Relationship.ContainsItem(relationship));
		}		
	}
}