using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Billing
{
	public class AutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var assemblies = new[] {Assembly.GetAssembly(typeof(AutofacModule))};
			
			builder.RegisterAssemblyTypes(assemblies)
				.Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();
			builder.RegisterAssemblyTypes(assemblies)
				.Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Helper")).AsImplementedInterfaces().AsSelf().SingleInstance();
			builder.RegisterAssemblyTypes(assemblies)
				.Where(t => t.Name.EndsWith("Factory")).AsImplementedInterfaces().AsSelf().SingleInstance().PropertiesAutowired();
		}
	}
}