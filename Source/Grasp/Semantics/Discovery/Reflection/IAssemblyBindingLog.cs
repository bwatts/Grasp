using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Semantics.Discovery.Reflection
{
	[ContractClass(typeof(IAssemblyBindingLogContract))]
	public interface IAssemblyBindingLog
	{
		void OnTraitDeclarationException(FieldInfo traitDeclaration, Exception exception);

		void OnFieldDeclarationException(FieldInfo fieldDeclaration, Exception exception);
	}

	[ContractClassFor(typeof(IAssemblyBindingLog))]
	internal abstract class IAssemblyBindingLogContract : IAssemblyBindingLog
	{
		void IAssemblyBindingLog.OnTraitDeclarationException(FieldInfo traitDeclaration, Exception exception)
		{
			Contract.Requires(traitDeclaration != null);
			Contract.Requires(exception != null);
		}

		void IAssemblyBindingLog.OnFieldDeclarationException(FieldInfo fieldDeclaration, Exception exception)
		{
			Contract.Requires(fieldDeclaration != null);
			Contract.Requires(exception != null);
		}
	}
}