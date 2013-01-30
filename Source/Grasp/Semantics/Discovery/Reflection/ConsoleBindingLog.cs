using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	public sealed class ConsoleBindingLog : IAssemblyBindingLog
	{
		public void OnTraitDeclarationException(FieldInfo traitDeclaration, Exception exception)
		{
			Console.Write(new StringBuilder("Exception occurred while accessing the trait declared by ")
				.Append(traitDeclaration.DeclaringType.FullName)
				.Append(".")
				.Append(traitDeclaration.Name)
				.AppendLine(":")
				.AppendLine()
				.Append(exception)
				.ToString());
		}

		public void OnFieldDeclarationException(FieldInfo fieldDeclaration, Exception exception)
		{
			Console.Write(new StringBuilder("Exception occurred while accessing the field declared by ")
				.Append(fieldDeclaration.DeclaringType.FullName)
				.Append(".")
				.Append(fieldDeclaration.Name)
				.AppendLine(":")
				.AppendLine()
				.Append(exception)
				.ToString());
		}
	}
}