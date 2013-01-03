using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dash.Applications.Definition;
using Grasp;

namespace Dash.Infrastructure
{
	/// <summary>
	/// Describes a service which installs application packages from designated sources
	/// </summary>
	[ContractClass(typeof(IInstallationServiceContract))]
	public interface IInstallationService
	{
		/// <summary>
		/// Installs the specified application in the specified environment by referencing packages from the specified sources
		/// </summary>
		/// <param name="workItemId">The identifer of the work item tracking the installation</param>
		/// <param name="environmentId">The identifier of the environment to which to add the application</param>
		/// <param name="applicationName">The name of the application to install</param>
		/// <param name="packageSourceLocations">The locations of the package sources containing application packages</param>
		/// <returns>The work of installing the application</returns>
		Task InstallApplicationAsync(EntityId workItemId, EntityId environmentId, FullName applicationName, IEnumerable<PackageSourceLocation> packageSourceLocations);
	}

	[ContractClassFor(typeof(IInstallationService))]
	internal abstract class IInstallationServiceContract : IInstallationService
	{
		Task IInstallationService.InstallApplicationAsync(EntityId workItemId, EntityId environmentId, FullName applicationName, IEnumerable<PackageSourceLocation> packageSourceLocations)
		{
			Contract.Requires(workItemId != EntityId.Unassigned);
			Contract.Requires(environmentId != EntityId.Unassigned);
			Contract.Requires(applicationName != null);
			Contract.Requires(packageSourceLocations != null);
			Contract.Ensures(Contract.Result<Task>() != null);

			return null;
		}
	}
}