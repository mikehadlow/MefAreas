using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Mike.MefAreas.Web.IoC;

namespace Mike.MefAreas.Web
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceVirtualPathProvider());
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            InitializeWindsor();
        }

        protected void Application_End()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        void InitializeWindsor()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This(),
                         FromAssembly.InDirectory(new AssemblyFilter(HttpRuntime.BinDirectory, "*AddIn.dll")));

            var controllerFactory = Container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        static IWindsorContainer container;
        
        public IWindsorContainer Container
        {
            get { return container; }
        }
    }
}