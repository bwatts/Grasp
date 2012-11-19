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
		public static T ReadContent<T>(this IEnumerable<MContent> contents, MClass @class) where T : MContent
		{
			Contract.Requires(contents != null);

			return contents.OfType<T>().First(content => content.Class.ContainsItem(@class));
		}

		public static T ReadValue<T>(this IEnumerable<MContent> contents, MClass @class)
		{
			Contract.Requires(contents != null);

			return contents.ReadContent<MValue>(@class).ReadValue<T>();
		}

		public static T ReadValue<T>(this MValue value)
		{
			Contract.Requires(value != null);

			return Conversion.To<T>(value.Object);
		}

		public static MLink ReadLink(this IEnumerable<MContent> contents, Relationship relationship)
		{
			Contract.Requires(contents != null);
			Contract.Requires(relationship != null);

			return contents.OfType<MLink>().First(content => content.Hyperlink.Relationship.ContainsItem(relationship));
		}		
	}
}