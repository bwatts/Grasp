using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Converters;

namespace Grasp.Raven
{
	// https://github.com/ravendb/ravendb/blob/master/Raven.Client.Lightweight/Converters/GuidConverter.cs

	public sealed class RavenEntityIdConverter : ITypeConverter
	{
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(EntityId);
		}

		public string ConvertFrom(string tag, object value, bool allowNull)
		{
			var id = (EntityId) value;

			if(id == EntityId.Unassigned)
			{
				id = EntityId.Generate();
			}

			return tag + id.ToString();
		}

		public object ConvertTo(string value)
		{
			return new EntityId(value);
		}
	}
}