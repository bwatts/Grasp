using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dash.Applications.Definition;
using Grasp;
using Grasp.Messaging;

namespace Dash.Infrastructure
{




	public interface IPackageServer
	{
		Task<InstallPackageResult> InstallPackageAsync(PackageResource package);

		Task<InstallPackageResult> UninstallPackageAsync(PackageResource package);
	}



	public class PackageResource : Notion
	{

	}


	public abstract class InstallPackageResult : Notion
	{

	}





	[InheritedExport]
	public interface IDashable
	{
		void ConfigureDashboard(Dashboard dashboard);
	}



	


	public sealed class InstallationService : Publisher, IInstallationService
	{
		public static readonly Field<IPackageServer> _packageServerField = Field.On<InstallationService>.For(x => x._packageServer);

		private IPackageServer _packageServer { get { return GetValue(_packageServerField); } set { SetValue(_packageServerField, value); } }

		public InstallationService(IPackageServer packageServer)
		{
			Contract.Requires(packageServer != null);

			_packageServer = packageServer;
		}

		public async Task InstallApplicationAsync(EntityId workItemId, EntityId environmentId, FullName applicationName, IEnumerable<PackageSourceLocation> packageSourceLocations)
		{
			// Get application from set enabled on Dash server (ApplicationResource)
			//
			//	<"Install" NuGet package(s)>
			//
			//	MEF discovers package elements
			//		Metadata describes handlers and subscribers
			//			Pattern matching?
			//
			//	API parts
			//		Routing
			//		Controllers
			//		Media types (formatters)
			//		Resources
			//
			//	if success
			//		await IssueAsync(new CommitInstallationCommand(workItemId, environmentId, applicationName));
			//	else
			//		await IssueAsync(new StopInstallationCommand(workItemId, environmentId, applicationName));
		}
	}
}