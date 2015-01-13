using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using iQuarc.AppBoot;
using Microsoft.Practices.ServiceLocation;

namespace iQuarc.Movies.Web
{
	public static class BootstrapConfig
	{
		public static IServiceLocator ServiceLocator { get; private set; }

		public static void Configure()
		{
			IList<Assembly> assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
				.Where(a => a.GetName().Name.StartsWith("iQuarc"))
				.ToList();

			Bootstrapper boostrapper = new UnityBootstrapper(assemblies);
			boostrapper.AddRegistrationBehavior(new ServiceRegistrationBehavior());

			boostrapper.Run();

			ServiceLocator = boostrapper.ServiceLocator;
		}
	}
}