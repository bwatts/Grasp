using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Knowledge;

namespace Grasp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class FieldContextBindingsToString
	{
		public static string BindingsToString(this IFieldContext fieldContext)
		{
			Contract.Requires(fieldContext != null);

			var text = new StringBuilder();

			foreach(var binding in fieldContext.GetBindings())
			{
				text.AppendValue(binding);
			}

			return text.ToString();
		}

		private static void AppendValue(this StringBuilder text, FieldBinding binding)
		{
			if(text.Length > 0)
			{
				text.AppendLine();
			}

			text.Append(".");

			if(binding.Field.Trait != null)
			{
				text.Append(binding.Field.OwnerType.FullName).Append(".");
			}

			text.Append(binding.Field.Name).Append(" = ");

			if(binding.Field.ValueType == typeof(string))
			{
				text.Append('"').Append(binding.Value).Append('"');
			}
			else if(binding.Field.ValueType == typeof(FullName))
			{
				text.Append("{").Append(binding.Value).Append("}");
			}
			else
			{
				text.Append(binding.Value);
			}
		}
	}
}